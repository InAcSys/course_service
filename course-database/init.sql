-- Crear extensión para UUIDs (si no existe)
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Tabla: AcademicPrograms
CREATE TABLE
    IF NOT EXISTS "AcademicPrograms" (
        "Id" SERIAL PRIMARY KEY,
        "Name" VARCHAR(255) NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL
    );

-- Tabla: AcademicLevels
CREATE TABLE
    IF NOT EXISTS "AcademicLevels" (
        "Id" SERIAL PRIMARY KEY,
        "Name" VARCHAR(255) NOT NULL,
        "AcademicProgramId" INT NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        CONSTRAINT "FK_AcademicPrograms" FOREIGN KEY ("AcademicProgramId") REFERENCES "AcademicPrograms" ("Id")
    );

-- Tabla: Courses
CREATE TABLE
    IF NOT EXISTS "Courses" (
        "Id" UUID PRIMARY KEY,
        "Name" VARCHAR(255) NOT NULL,
        "AcademicLevelId" INT NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        CONSTRAINT "FK_AcademicLevels" FOREIGN KEY ("AcademicLevelId") REFERENCES "AcademicLevels" ("Id")
    );

-- Tabla: Subjects
CREATE TABLE
    IF NOT EXISTS "Subjects" (
        "Id" UUID PRIMARY KEY,
        "Name" VARCHAR(255) NOT NULL,
        "ShortName" VARCHAR(100) NOT NULL,
        "Code" VARCHAR(100) NOT NULL,
        "LMSId" INT NOT NULL,
        "CourseId" UUID NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        CONSTRAINT "FK_Courses" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id")
    );