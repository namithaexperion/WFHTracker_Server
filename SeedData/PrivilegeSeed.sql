BEGIN
    BEGIN TRY
        BEGIN TRANSACTION TRANS;

        ------------------------------------------------------------------
        -- Admin Role
        ------------------------------------------------------------------
        IF NOT EXISTS (
            SELECT 1
            FROM AccessRoles
            WHERE RoleName = 'Admin'
        )
        BEGIN
            SET IDENTITY_INSERT AccessRoles ON;

            INSERT INTO AccessRoles
            (
                AccessRoleId,
                RoleName,
                CreatedDate,
                CreatedBy,
                UpdatedDate,
                UpdatedBy
            )
            VALUES
            (
                1,
                'Admin',
                GETUTCDATE(),
                1,
                GETUTCDATE(),
                1
            );

            SET IDENTITY_INSERT AccessRoles OFF;
        END

        ------------------------------------------------------------------
        -- Admin Privilege
        ------------------------------------------------------------------
        IF NOT EXISTS (
            SELECT 1
            FROM Privileges
            WHERE PrivilegeKey = 'admin'
        )
        BEGIN
            SET IDENTITY_INSERT Privileges ON;

            INSERT INTO Privileges
            (
                PrivilegeId,
                PrivilegeName,
                PrivilegeKey,
                CreatedDate,
                CreatedBy,
                UpdatedDate,
                UpdatedBy
            )
            VALUES
            (
                1,
                'Admin',
                'admin',
                GETUTCDATE(),
                1,
                GETUTCDATE(),
                1
            );

            SET IDENTITY_INSERT Privileges OFF;
        END


        ------------------------------------------------------------------
        -- Role ↔ Privilege Mapping
        ------------------------------------------------------------------
        IF NOT EXISTS (
            SELECT 1
            FROM PrivilegeRoles
            WHERE AccessRoleId = 1
            AND PrivilegeId = 1
        )
        BEGIN
            SET IDENTITY_INSERT PrivilegeRoles ON;

            INSERT INTO PrivilegeRoles
            (
                PrivilegeRoleId,
                AccessRoleId,
                PrivilegeId,
                CreatedDate,
                CreatedBy,
                UpdatedDate,
                UpdatedBy
            )
            VALUES
            (
                1,
                1,
                1,
                GETUTCDATE(),
                1,
                GETUTCDATE(),
                1
            );

            SET IDENTITY_INSERT PrivilegeRoles OFF;
        END

        ------------------------------------------------------------------
        -- User ↔ Role Mapping
        ------------------------------------------------------------------
        IF NOT EXISTS (
            SELECT 1
            FROM UserRoles
            WHERE ResourceId = 1
            AND AccessRoleId = 1
        )
        BEGIN
            SET IDENTITY_INSERT UserRoles ON;

            INSERT INTO UserRoles
            (
                UserRoleId,
                ResourceId,
                AccessRoleId,
                CreatedDate,
                CreatedBy,
                UpdatedDate,
                UpdatedBy
            )
            VALUES
            (
                1,
                1,
                1,
                GETUTCDATE(),
                1,
                GETUTCDATE(),
                1
            );

            SET IDENTITY_INSERT UserRoles OFF;
        END

        COMMIT TRANSACTION TRANS;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION TRANS;
        THROW;
    END CATCH
END