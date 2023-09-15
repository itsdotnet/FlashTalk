-- Create the User table
CREATE TABLE "User" (
    "Id" serial PRIMARY KEY,
    "Name" varchar(255) NOT NULL,
    "Username" varchar(255) NOT NULL,
    "Password" varchar(255) NOT NULL,
    "IsDeleted" boolean DEFAULT false,
    "CreatedAt" timestamp NOT NULL,
    "UpdatedAt" timestamp NOT NULL
);

-- Create the Message table
CREATE TABLE "Message" (
    "Id" serial PRIMARY KEY,
    "From" bigint NOT NULL,
    "To" bigint NOT NULL,
    "Text" text NOT NULL,
    "IsDeleted" boolean DEFAULT false,
    "CreatedAt" timestamp NOT NULL,
    "UpdatedAt" timestamp NOT NULL,
    FOREIGN KEY ("From") REFERENCES "User" ("Id"),
    FOREIGN KEY ("To") REFERENCES "User" ("Id")
);
