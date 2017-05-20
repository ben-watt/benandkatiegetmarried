
CREATE SCHEMA core


CREATE TABLE core.Events 
(
    "Id" uuid NOT NULL,
    "EventName" character varying(500),
	"EventType" character varying(200),
    "StartTime" timestamp,
    "EndTime" timestamp,
	CONSTRAINT events_pkey PRIMARY KEY ("Id")
)

CREATE TABLE core.Weddings
(
    "Id" uuid NOT NULL,
    "Bride" character varying(200) NOT NULL,
    "Groom" character varying(200) NOT NULL,
    CONSTRAINT weddings_pkey PRIMARY KEY ("Id")
)

CREATE TABLE core.Venues 
(
	"Id" uuid NOT NULL,
	"eventId" uuid NOT NULL,
	"Name" character varying(500) NOT NULL,
	"AddressLine1" character varying(500) NULL,
	"AddressLine2" character varying(500) NULL,
	"Town" character varying(500) NULL,
	"County" character varying(500) NULL,
	"PostCode" character varying(500) NULL,
	"Country" character varying(500) NULL,
	"Telephone" character varying(500) NULL,
	"Website" character varying(500) NULL,
	CONSTRAINT venues_pkey PRIMARY KEY ("Id")
)

CREATE TABLE core.Invites
(
	"Id" uuid NOT NULL,
	"Password" character varying NOT NULL,
	"Greeting" character varying (500) NOT NULL,
	CONSTRAINT invites_pkey PRIMARY KEY ("Id")
)

CREATE TABLE core.Guests
(
	"Id" uuid NOT NULL,
	"FirstName" character varying (200) NOT NULL,
	"LastName" character varying (200) NOT NULL,
	"UserName" character varying NULL,
	"Type" character varying(200) NOT NULL,
	"IsFeatured" bool default false NOT NULL,
	CONSTRAINT guests_pkey PRIMARY KEY ("Id")
)