// unobtrusive validation support library for jquery and jquery validate
// copyright (c) .net foundation. all rights reserved.
// licensed under the apache license, version 2.0. see license.txt in the project root for license information.
// @version v3.2.11

/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, newcap: true, immed: true, strict: false */
/*global document: false, jquery: false */

(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // amd. register as an anonymous module.
        define("jquery.validate.unobtrusive", ['jquery-validation'], factory);
    } else if (typeof module === 'object' && module.exports) {
        // commonjs-like environments that support module.exports     
        module.exports = factory(require('jquery-validation'));
    } else {
        // browser global
        jquery.validator.unobtrusive = factory(jquery);
    }
}(function ($) {
    var $jqval = $.validator,
        adapters,
        data_validation = "unobtrusivevalidation";

    function setvalidationvalues(options, rulename, value) {
        options.rules[rulename] = value;
        if (options.message) {
            options.messages[rulename] = options.message;
        }
    }

    function splitandtrim(value) {
        return value.replace(/^\s+|\s+$/g, "").split(/\s*,\s*/g);
    }

    function escapeattributevalue(value) {
        // as mentioned on http://api.jquery.com/category/selectors/
        return value.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1");
    }

    function getmodelprefix(fieldname) {
        return fieldname.substr(0, fieldname.lastindexof(".") + 1);
    }

    function appendmodelprefix(value, prefix) {
        if (value.indexof("*.") === 0) {
            value = value.replace("*.", prefix);
        }
        return value;
    }

    function onerror(error, inputelement) {  // 'this' is the form element
        var container = $(this).find("[data-valmsg-for='" + escapeattributevalue(inputelement[0].name) + "']"),
            replaceattrvalue = container.attr("data-valmsg-replace"),
            replace = replaceattrvalue ? $.parsejson(replaceattrvalue) !== false : null;

        container.removeclass("field-validation-valid").addclass("field-validation-error");
        error.data("unobtrusivecontainer", container);

        if (replace) {
            container.empty();
            error.removeclass("input-validation-error").appendto(container);
        }
        else {
            error.hide();
        }
    }

    function onerrors(event, validator) {  // 'this' is the form element
        var container = $(this).find("[data-valmsg-summary=true]"),
            list = container.find("ul");

        if (list && list.length && validator.errorlist.length) {
            list.empty();
            container.addclass("validation-summary-errors").removeclass("validation-summary-valid");

            $.each(validator.errorlist, function () {
                $("<li />").html(this.message).appendto(list);
            });
        }
    }

    function onsuccess(error) {  // 'this' is the form element
        var container = error.data("unobtrusivecontainer");

        if (container) {
            var replaceattrvalue = container.attr("data-valmsg-replace"),
                replace = replaceattrvalue ? $.parsejson(replaceattrvalue) : null;

            container.addclass("field-validation-valid").removeclass("field-validation-error");
            error.removedata("unobtrusivecontainer");

            if (replace) {
                container.empty();
            }
        }
    }

    function onreset(event) {  // 'this' is the form element
        var $form = $(this),
            key = '__jquery_unobtrusive_validation_form_reset';
        if ($form.data(key)) {
            return;
        }
        // set a flag that indicates we're currently resetting the form.
        $form.data(key, true);
        try {
            $form.data("validator").resetform();
        } finally {
            $form.removedata(key);
        }

        $form.find(".validation-summary-errors")
            .addclass("validation-summary-valid")
            .removeclass("validation-summary-errors");
        $form.find(".field-validation-error")
            .addclass("field-validation-valid")
            .removeclass("field-validation-error")
            .removedata("unobtrusivecontainer")
            .find(">*")  // if we were using valmsg-replace, get the underlying error
            .removedata("unobtrusivecontainer");
    }

    function validationinfo(form) {
        var $form = $(form),
            result = $form.data(data_validation),
            onresetproxy = $.proxy(onreset, form),
            defaultoptions = $jqval.unobtrusive.options || {},
            execincontext = function (name, args) {
                var func = defaultoptions[name];
                func && $.isfunction(func) && func.apply(form, args);
            };

        if (!result) {
            result = {
                options: {  // options structure passed to jquery validate's validate() method
                    errorclass: defaultoptions.errorclass || "input-validation-error",
                    errorelement: defaultoptions.errorelement || "span",
                    errorplacement: function () {
                        onerror.apply(form, arguments);
                        execincontext("errorplacement", arguments);
                    },
                    invalidhandler: function () {
                        onerrors.apply(form, arguments);
                        execincontext("invalidhandler", arguments);
                    },
                    messages: {},
                    rules: {},
                    success: function () {
                        onsuccess.apply(form, arguments);
                        execincontext("success", arguments);
                    }
                },
                attachvalidation: function () {
                    $form
                        .off("reset." + data_validation, onresetproxy)
                        .on("reset." + data_validation, onresetproxy)
                        .validate(this.options);
                },
                validate: function () {  // a validation function that is called by unobtrusive ajax
                    $form.validate();
                    return $form.valid();
                }
            };
            $form.data(data_validation, result);
        }

        return result;
    }

    $jqval.unobtrusive = {
        adapters: [],

        parseelement: function (element, skipattach) {
            /// <summary>
            /// parses a single html element for unobtrusive validation attributes.
            /// </summary>
            /// <param name="element" domelement="true">the html element to be parsed.</param>
            /// <param name="skipattach" type="boolean">[optional] true to skip attaching the
            /// validation to the form. if parsing just this single element, you should specify true.
            /// if parsing several elements, you should specify false, and manually attach the validation
            /// to the form when you are finished. the default is false.</param>
            var $element = $(element),
                form = $element.parents("form")[0],
                valinfo, rules, messages;

            if (!form) {  // cannot do client-side validation without a form
                return;
            }

            valinfo = validationinfo(form);
            valinfo.options.rules[element.name] = rules = {};
            valinfo.options.messages[element.name] = messages = {};

            $.each(this.adapters, function () {
                var prefix = "data-val-" + this.name,
                    message = $element.attr(prefix),
                    paramvalues = {};

                if (message !== undefined) {  // compare against undefined, because an empty message is legal (and falsy)
                    prefix += "-";

                    $.each(this.params, function () {
                        paramvalues[this] = $element.attr(prefix + this);
                    });

                    this.adapt({
                        element: element,
                        form: form,
                        message: message,
                        params: paramvalues,
                        rules: rules,
                        messages: messages
                    });
                }
            });

            $.extend(rules, { "__dummy__": true });

            if (!skipattach) {
                valinfo.attachvalidation();
            }
        },

        parse: function (selector) {
            /// <summary>
            /// parses all the html elements in the specified selector. it looks for input elements decorated
            /// with the [data-val=true] attribute value and enables validation according to the data-val-*
            /// attribute values.
            /// </summary>
            /// <param name="selector" type="string">any valid jquery selector.</param>

            // $forms includes all forms in selector's dom hierarchy (parent, children and self) that have at least one
            // element with data-val=true
            var $selector = $(selector),
                $forms = $selector.parents()
                    .addback()
                    .filter("form")
                    .add($selector.find("form"))
                    .has("[data-val=true]");

            $selector.find("[data-val=true]").each(function () {
                $jqval.unobtrusive.parseelement(this, true);
            });

            $forms.each(function () {
                var info = validationinfo(this);
                if (info) {
                    info.attachvalidation();
                }
            });
        }
    };

    adapters = $jqval.unobtrusive.adapters;

    adapters.add = function (adaptername, params, fn) {
        /// <summary>adds a new adapter to convert unobtrusive html into a jquery validate validation.</summary>
        /// <param name="adaptername" type="string">the name of the adapter to be added. this matches the name used
        /// in the data-val-nnnn html attribute (where nnnn is the adapter name).</param>
        /// <param name="params" type="array" optional="true">[optional] an array of parameter names (strings) that will
        /// be extracted from the data-val-nnnn-mmmm html attributes (where nnnn is the adapter name, and
        /// mmmm is the parameter name).</param>
        /// <param name="fn" type="function">the function to call, which adapts the values from the html
        /// attributes into jquery validate rules and/or messages.</param>
        /// <returns type="jquery.validator.unobtrusive.adapters" />
        if (!fn) {  // called with no params, just a function
            fn = params;
            params = [];
        }
        this.push({ name: adaptername, params: params, adapt: fn });
        return this;
    };

    adapters.addbool = function (adaptername, rulename) {
        /// <summary>adds a new adapter to convert unobtrusive html into a jquery validate validation, where
        /// the jquery validate validation rule has no parameter values.</summary>
        /// <param name="adaptername" type="string">the name of the adapter to be added. this matches the name used
        /// in the data-val-nnnn html attribute (where nnnn is the adapter name).</param>
        /// <param name="rulename" type="string" optional="true">[optional] the name of the jquery validate rule. if not provided, the value
        /// of adaptername will be used instead.</param>
        /// <returns type="jquery.validator.unobtrusive.adapters" />
        return this.add(adaptername, function (options) {
            setvalidationvalues(options, rulename || adaptername, true);
        });
    };

    adapters.addminmax = function (adaptername, minrulename, maxrulename, minmaxrulename, minattribute, maxattribute) {
        /// <summary>adds a new adapter to convert unobtrusive html into a jquery validate validation, where
        /// the jquery validate validation has three potential rules (one for min-only, one for max-only, and
        /// one for min-and-max). the html parameters are expected to be named -min and -max.</summary>
        /// <param name="adaptername" type="string">the name of the adapter to be added. this matches the name used
        /// in the data-val-nnnn html attribute (where nnnn is the adapter name).</param>
        /// <param name="minrulename" type="string">the name of the jquery validate rule to be used when you only
        /// have a minimum value.</param>
        /// <param name="maxrulename" type="string">the name of the jquery validate rule to be used when you only
        /// have a maximum value.</param>
        /// <param name="minmaxrulename" type="string">the name of the jquery validate rule to be used when you
        /// have both a minimum and maximum value.</param>
        /// <param name="minattribute" type="string" optional="true">[optional] the name of the html attribute that
        /// contains the minimum value. the default is "min".</param>
        /// <param name="maxattribute" type="string" optional="true">[optional] the name of the html attribute that
        /// contains the maximum value. the default is "max".</param>
        /// <returns type="jquery.validator.unobtrusive.adapters" />
        return this.add(adaptername, [minattribute || "min", maxattribute || "max"], function (options) {
            var min = options.params.min,
                max = options.params.max;

            if (min && max) {
                setvalidationvalues(options, minmaxrulename, [min, max]);
            }
            else if (min) {
                setvalidationvalues(options, minrulename, min);
            }
            else if (max) {
                setvalidationvalues(options, maxrulename, max);
            }
        });
    };

    adapters.addsingleval = function (adaptername, attribute, rulename) {
        /// <summary>adds a new adapter to convert unobtrusive html into a jquery validate validation, where
        /// the jquery validate validation rule has a single value.</summary>
        /// <param name="adaptername" type="string">the name of the adapter to be added. this matches the name used
        /// in the data-val-nnnn html attribute(where nnnn is the adapter name).</param>
        /// <param name="attribute" type="string">[optional] the name of the html attribute that contains the value.
        /// the default is "val".</param>
        /// <param name="rulename" type="string" optional="true">[optional] the name of the jquery validate rule. if not provided, the value
        /// of adaptername will be used instead.</param>
        /// <returns type="jquery.validator.unobtrusive.adapters" />
        return this.add(adaptername, [attribute || "val"], function (options) {
            setvalidationvalues(options, rulename || adaptername, options.params[attribute]);
        });
    };

    $jqval.addmethod("__dummy__", function (value, element, params) {
        return true;
    });

    $jqval.addmethod("regex", function (value, element, params) {
        var match;
        if (this.optional(element)) {
            return true;
        }

        match = new regexp(params).exec(value);
        return (match && (match.index === 0) && (match[0].length === value.length));
    });

    $jqval.addmethod("nonalphamin", function (value, element, nonalphamin) {
        var match;
        if (nonalphamin) {
            match = value.match(/\w/g);
            match = match && match.length >= nonalphamin;
        }
        return match;
    });

    if ($jqval.methods.extension) {
        adapters.addsingleval("accept", "mimtype");
        adapters.addsingleval("extension", "extension");
    } else {
        // for backward compatibility, when the 'extension' validation method does not exist, such as with versions
        // of jquery validation plugin prior to 1.10, we should use the 'accept' method for
        // validating the extension, and ignore mime-type validations as they are not supported.
        adapters.addsingleval("extension", "extension", "accept");
    }

    adapters.addsingleval("regex", "pattern");
    adapters.addbool("creditcard").addbool("date").addbool("digits").addbool("email").addbool("number").addbool("url");
    adapters.addminmax("length", "minlength", "maxlength", "rangelength").addminmax("range", "min", "max", "range");
    adapters.addminmax("minlength", "minlength").addminmax("maxlength", "minlength", "maxlength");
    adapters.add("equalto", ["other"], function (options) {
        var prefix = getmodelprefix(options.element.name),
            other = options.params.other,
            fullothername = appendmodelprefix(other, prefix),
            element = $(options.form).find(":input").filter("[name='" + escapeattributevalue(fullothername) + "']")[0];

        setvalidationvalues(options, "equalto", element);
    });
    adapters.add("required", function (options) {
        // jquery validate equates "required" with "mandatory" for checkbox elements
        if (options.element.tagname.touppercase() !== "input" || options.element.type.touppercase() !== "checkbox") {
            setvalidationvalues(options, "required", true);
        }
    });
    adapters.add("remote", ["url", "type", "additionalfields"], function (options) {
        var value = {
            url: options.params.url,
            type: options.params.type || "get",
            data: {}
        },
            prefix = getmodelprefix(options.element.name);

        $.each(splitandtrim(options.params.additionalfields || options.element.name), function (i, fieldname) {
            var paramname = appendmodelprefix(fieldname, prefix);
            value.data[paramname] = function () {
                var field = $(options.form).find(":input").filter("[name='" + escapeattributevalue(paramname) + "']");
                // for checkboxes and radio buttons, only pick up values from checked fields.
                if (field.is(":checkbox")) {
                    return field.filter(":checked").val() || field.filter(":hidden").val() || '';
                }
                else if (field.is(":radio")) {
                    return field.filter(":checked").val() || '';
                }
                return field.val();
            };
        });

        setvalidationvalues(options, "remote", value);
    });
    adapters.add("password", ["min", "nonalphamin", "regex"], function (options) {
        if (options.params.min) {
            setvalidationvalues(options, "minlength", options.params.min);
        }
        if (options.params.nonalphamin) {
            setvalidationvalues(options, "nonalphamin", options.params.nonalphamin);
        }
        if (options.params.regex) {
            setvalidationvalues(options, "regex", options.params.regex);
        }
    });
    adapters.add("fileextensions", ["extensions"], function (options) {
        setvalidationvalues(options, "extension", options.params.extensions);
    });

    $(function () {
        $jqval.unobtrusive.parse(document);
    });

    return $jqval.unobtrusive;
}));
