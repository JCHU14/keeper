CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg VARCHAR(255) COMMENT 'Cover Img'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL primary key COMMENT 'primary key' AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        name CHAR(255) NOT NULL,
        img VARCHAR(1000) NOT NULL,
        description VARCHAR(500) NOT NULL,
        -- vaultId INT NOT NULL,
        -- Foreign Key (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        visits INT NOT NULL DEFAULT 0,
        -- accountId VARCHAR(255) NOT NULL,
        -- Foreign Key (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

-- **FIXME - SECTION Come back and add the kept**

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL primary key COMMENT 'primary key' AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        name CHAR(255) NOT NULL,
        img VARCHAR(1000) NOT NULL,
        description VARCHAR(255) NOT NULL,
        isPrivate BOOL NOT NULL DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeep(
        id INT NOT NULL primary key COMMENT 'primary key' AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (vaultId) REFERENCES vaults(id),
        FOREIGN KEY (keepId) REFERENCES keeps(id),
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP Table

SELECT keep.*, acc.*
FROM keeps keep
    JOIN accounts acc ON acc.id = keep.creatorId
WHERE keep.id = 1;