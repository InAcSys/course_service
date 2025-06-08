-- Crear extensión para UUIDs (si no existe)
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Tabla: AcademicPrograms
CREATE TABLE
    IF NOT EXISTS "AcademicPrograms" (
        "Id" SERIAL PRIMARY KEY,
        "Name" VARCHAR(255) NOT NULL,
        "Description" VARCHAR(255) NOT NULL,
        "Code" VARCHAR(100) NOT NULL,
        "DegreeType" VARCHAR(100) NULL,
        "DurationType" VARCHAR(100) NOT NULL,
        "Periods" INT NOT NULL,
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
        "Description" VARCHAR(255) NOT NULL,
        "Code" VARCHAR(100) NOT NULL,
        "Order" INT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL
    );

-- Tabla: Courses
CREATE TABLE
    IF NOT EXISTS "Courses" (
        "Id" UUID PRIMARY KEY,
        "Name" VARCHAR(255) NOT NULL,
        "Description" VARCHAR(255) NOT NULL,
        "Code" VARCHAR(100) NOT NULL,
        "AcademicPeriod" VARCHAR(10) NOT NULL,
        "AcademicLevelId" INT NOT NULL,
        "AcademicProgramId" INT NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        FOREIGN KEY ("AcademicLevelId") REFERENCES "AcademicLevels" ("Id") ON DELETE CASCADE,
        FOREIGN KEY ("AcademicProgramId") REFERENCES "AcademicPrograms" ("Id") ON DELETE CASCADE
    );

-- Tabla: Subjects
CREATE TABLE
    IF NOT EXISTS "Subjects" (
        "Id" UUID PRIMARY KEY,
        "Name" VARCHAR(255) NOT NULL,
        "Description" VARCHAR(100) NOT NULL,
        "Code" VARCHAR(100) NOT NULL,
        "Credits" INT NOT NULL,
        "LMSId" INT NULL,
        "AcademicLevelId" INT NOT NULL,
        "TeacherId" UUID NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        FOREIGN KEY ("AcademicLevelId") REFERENCES "AcademicLevels" ("Id") ON DELETE CASCADE
    );

CREATE TABLE
    IF NOT EXISTS "SubjectPrograms" (
        "Id" SERIAL PRIMARY KEY,
        "SubjectId" UUID NOT NULL,
        "AcademicProgramId" INT NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        FOREIGN KEY ("SubjectId") REFERENCES "Subjects" ("Id") ON DELETE CASCADE,
        FOREIGN KEY ("AcademicProgramId") REFERENCES "AcademicPrograms" ("Id") ON DELETE CASCADE
    );

CREATE TABLE
    IF NOT EXISTS "SubjectRequisites" (
        "Id" SERIAL PRIMARY KEY,
        "SubjectId" UUID NOT NULL,
        "RequisteId" UUID NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        FOREIGN KEY ("SubjectId") REFERENCES "Subjects" ("Id") ON DELETE CASCADE,
        FOREIGN KEY ("RequisteId") REFERENCES "Subjects" ("Id") ON DELETE CASCADE
    );

CREATE TABLE
    IF NOT EXISTS "CourseSubjects" (
        "Id" SERIAL PRIMARY KEY,
        "CourseId" UUID NOT NULL,
        "SubjectId" UUID NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id") ON DELETE CASCADE,
        FOREIGN KEY ("SubjectId") REFERENCES "Subjects" ("Id") ON DELETE CASCADE
    );

CREATE TABLE
    IF NOT EXISTS "ProgramSubjects" (
        "Id" SERIAL PRIMARY KEY,
        "ProgramId" INT NOT NULL,
        "SubjectId" UUID NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        FOREIGN KEY ("ProgramId") REFERENCES "AcademicPrograms" ("Id") ON DELETE CASCADE,
        FOREIGN KEY ("SubjectId") REFERENCES "Subjects" ("Id") ON DELETE CASCADE
    );

CREATE TABLE
    IF NOT EXISTS "ProgramLevels" (
        "Id" SERIAL PRIMARY KEY,
        "ProgramId" INT NOT NULL,
        "LevelId" INT NOT NULL,
        "IsActive" BOOLEAN NOT NULL,
        "Created" TIMESTAMP NOT NULL,
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL,
        FOREIGN KEY ("ProgramId") REFERENCES "AcademicPrograms" ("Id") ON DELETE CASCADE,
        FOREIGN KEY ("LevelId") REFERENCES "AcademicLevels" ("Id") ON DELETE CASCADE
    );