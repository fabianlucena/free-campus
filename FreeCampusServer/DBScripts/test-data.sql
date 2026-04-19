DECLARE @Id BIGINT = (SELECT Id FROM auth.Users WHERE Username = 'system');
DECLARE @Now DATETIME2 = GETUTCDATE();
DECLARE @1234hash NVARCHAR(255) = '100000.He2nIoHKO5PDiudeF3GV1Q==.OXZML34kQ8gPcsX01odwNpaNmNMkMzlggv5pLKqzekg=';

INSERT INTO auth.Users (
    Uuid, CreatedAt, UpdatedAt, DeletedAt,
    CreatedById, UpdatedById, DeletedById,
    Username, DisplayName, Email, PasswordHash,
    IsActive, CanLogin, LastLogin
)
VALUES
(NEWID(), @Now, @Now, NULL,
 1, 1, NULL,
 'admin', 'Administrador', 'admin@example.com', @1234hash,
 1, 1, NULL),

(NEWID(), @Now, @Now, NULL,
 1, 1, NULL,
 'jdoe', 'John Doe', 'jdoe@example.com', @1234hash,
 1, 1, NULL),

(NEWID(), @Now, @Now, NULL,
 1, 1, NULL,
 'mgarcia', 'María García', 'mgarcia@example.com', @1234hash,
 1, 1, NULL);

GO