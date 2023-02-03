IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE TABLE [Provincias] (
        [Id] int NOT NULL,
        [Nombre] nvarchar(max) NOT NULL,
        [ProvinciaId] int NULL,
        CONSTRAINT [PK_Provincias] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Provincias_Provincias_ProvinciaId] FOREIGN KEY ([ProvinciaId]) REFERENCES [Provincias] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE TABLE [Municipios] (
        [Id] int NOT NULL,
        [Nombre] nvarchar(max) NULL,
        [Provincia_Id] int NOT NULL,
        [MunicipioId] int NULL,
        CONSTRAINT [PK_Municipios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Municipios_Municipios_MunicipioId] FOREIGN KEY ([MunicipioId]) REFERENCES [Municipios] ([Id]),
        CONSTRAINT [FK_Municipios_Provincias_Provincia_Id] FOREIGN KEY ([Provincia_Id]) REFERENCES [Provincias] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE TABLE [Localidades] (
        [Id] bigint NOT NULL,
        [Nombre] nvarchar(max) NULL,
        [Municipio_Id] int NOT NULL,
        [LocalidadId] bigint NULL,
        CONSTRAINT [PK_Localidades] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Localidades_Localidades_LocalidadId] FOREIGN KEY ([LocalidadId]) REFERENCES [Localidades] ([Id]),
        CONSTRAINT [FK_Localidades_Municipios_Municipio_Id] FOREIGN KEY ([Municipio_Id]) REFERENCES [Municipios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE TABLE [Barrios] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        [Localidad_Id] bigint NOT NULL,
        CONSTRAINT [PK_Barrios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Barrios_Localidades_Localidad_Id] FOREIGN KEY ([Localidad_Id]) REFERENCES [Localidades] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE TABLE [Lotes] (
        [Id] int NOT NULL IDENTITY,
        [Numero] nvarchar(max) NOT NULL,
        [Metros] nvarchar(max) NOT NULL,
        [Etapa] nvarchar(max) NOT NULL,
        [Orientacion] int NOT NULL,
        [Disponible] bit NOT NULL,
        [Precio] int NOT NULL,
        [Barrio_Id] int NOT NULL,
        CONSTRAINT [PK_Lotes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Lotes_Barrios_Barrio_Id] FOREIGN KEY ([Barrio_Id]) REFERENCES [Barrios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE TABLE [Tipos] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        [Lote_Id] int NOT NULL,
        CONSTRAINT [PK_Tipos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tipos_Lotes_Lote_Id] FOREIGN KEY ([Lote_Id]) REFERENCES [Lotes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Barrios_Localidad_Id] ON [Barrios] ([Localidad_Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Localidades_LocalidadId] ON [Localidades] ([LocalidadId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Localidades_Municipio_Id] ON [Localidades] ([Municipio_Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Lotes_Barrio_Id] ON [Lotes] ([Barrio_Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Municipios_MunicipioId] ON [Municipios] ([MunicipioId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Municipios_Provincia_Id] ON [Municipios] ([Provincia_Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Provincias_ProvinciaId] ON [Provincias] ([ProvinciaId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    CREATE INDEX [IX_Tipos_Lote_Id] ON [Tipos] ([Lote_Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119171126_Primera Migracion con Entidades establecidas')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230119171126_Primera Migracion con Entidades establecidas', N'6.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Nombre] nvarchar(50) NOT NULL,
        [Apellido] nvarchar(max) NOT NULL,
        [Alias] nvarchar(3) NOT NULL,
        [Telefono] int NOT NULL,
        [Email] nvarchar(256) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120032509_Agrego Identity para manejo de usuario y roles')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230120032509_Agrego Identity para manejo de usuario y roles', N'6.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120063145_Actualizo tabla Usuario Tel')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Telefono');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Telefono] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120063145_Actualizo tabla Usuario Tel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230120063145_Actualizo tabla Usuario Tel', N'6.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120154556_Lote Fk Usuario')
BEGIN
    ALTER TABLE [Lotes] ADD [Usuario_Id] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120154556_Lote Fk Usuario')
BEGIN
    CREATE INDEX [IX_Lotes_Usuario_Id] ON [Lotes] ([Usuario_Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120154556_Lote Fk Usuario')
BEGIN
    ALTER TABLE [Lotes] ADD CONSTRAINT [FK_Lotes_AspNetUsers_Usuario_Id] FOREIGN KEY ([Usuario_Id]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230120154556_Lote Fk Usuario')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230120154556_Lote Fk Usuario', N'6.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230121021724_Agrego Fecha a la tabla Lote')
BEGIN
    ALTER TABLE [Lotes] ADD [FechaCreacion] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230121021724_Agrego Fecha a la tabla Lote')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230121021724_Agrego Fecha a la tabla Lote', N'6.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122013050_Telefono no requerido')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Telefono');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Telefono] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122013050_Telefono no requerido')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230122013050_Telefono no requerido', N'6.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230124014326_Requerimientos tabla municipio y localidad')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Municipios]') AND [c].[name] = N'Nombre');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Municipios] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Municipios] ALTER COLUMN [Nombre] nvarchar(max) NOT NULL;
    ALTER TABLE [Municipios] ADD DEFAULT N'' FOR [Nombre];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230124014326_Requerimientos tabla municipio y localidad')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Localidades]') AND [c].[name] = N'Nombre');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Localidades] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Localidades] ALTER COLUMN [Nombre] nvarchar(max) NOT NULL;
    ALTER TABLE [Localidades] ADD DEFAULT N'' FOR [Nombre];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230124014326_Requerimientos tabla municipio y localidad')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Alias');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Alias] nvarchar(3) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230124014326_Requerimientos tabla municipio y localidad')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230124014326_Requerimientos tabla municipio y localidad', N'6.0.12');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230124020518_El als e requerido')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Alias');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Alias] nvarchar(3) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [Alias];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230124020518_El als e requerido')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230124020518_El als e requerido', N'6.0.12');
END;
GO

COMMIT;
GO

