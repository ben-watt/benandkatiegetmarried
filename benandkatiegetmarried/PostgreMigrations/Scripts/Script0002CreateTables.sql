
CREATE SCHEMA core

CREATE TABLE core.Events 
(
	Id uniqueidentifier NOT NULL,
	Name character varying(500),
	Type character varying(200),
	StartTime datetime2,
	EndTime datetime2,
	CONSTRAINT events_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Weddings
(
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	Bride character varying(200) NOT NULL,
	Groom character varying(200) NOT NULL,
	CONSTRAINT weddings_pkey PRIMARY KEY (EventId)
)

CREATE TABLE core.Venues 
(
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	Name character varying(500) NOT NULL,
	AddressLine1 character varying(500) NULL,
	AddressLine2 character varying(500) NULL,
	Town character varying(500) NULL,
	County character varying(500) NULL,
	PostCode character varying(500) NULL,
	Country character varying(500) NULL,
	Telephone character varying(500) NULL,
	Website character varying(500) NULL,
	CONSTRAINT venues_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Invites
(
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	SecurityCode varchar(max) NOT NULL, 
	Password varchar(max) NOT NULL,
	Greeting varchar(max) NULL,
	Type character varying(200) NOT NULL,
	LoginAttempts int DEFAULT 0,
	CONSTRAINT invites_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Guests
(
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	InviteId uniqueidentifier NOT NULL references core.Invites(Id),
	FirstName character varying (200) NOT NULL,
	LastName character varying (200) NOT NULL,
	IsFeatured bit default 0 NOT NULL,
	HasSentRsvp bit default 0 NOT NULL,
	CONSTRAINT guests_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.RSVPs
(
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	InviteId uniqueidentifier NOT NULL references core.Invites(Id),
	GuestId uniqueidentifier NOT NULL references core.Guests(Id),
	Response bit DEFAULT NULL,
	ResponseMessage varchar(max) NULL,
	DietaryRequirements character varying (500) NULL,
	SongLink character varying (500) NULL,
	CONSTRAINT RSVP_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Meals (
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	Name character varying (300) NOT NULL,
	Course character varying (200) NULL,
	MealGroup character varying (200) NULL,
	CONSTRAINT meals_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.MealChoises 
(
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	GuestId uniqueidentifier NOT NULL references core.Guests(Id),
	RSVPId uniqueidentifier NOT NULL references core.RSVPs(Id),
	MealId uniqueidentifier NOT NULL references core.Meals(Id),
	CONSTRAINT mealChoises_pKey PRIMARY KEY (Id)
)

CREATE TABLE core.Users (
	Id uniqueidentifier NOT NULL,
	UserName varchar(max) NOT NULL,
	Password varchar(max) NOT NULL,
	LoginAttempts int DEFAULT 0,
	CONSTRAINT users_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.UserEventMapping (
	Id uniqueidentifier NOT NULL,
	UserId uniqueidentifier NOT NULL references core.Users(Id),
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	CONSTRAINT userEventMapping_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.MessageBoard (
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	Name character varying (200) NOT NULL,
	CONSTRAINT messageBoard_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Messages (
	Id uniqueidentifier NOT NULL,
	MessageBoardId uniqueidentifier NOT NULL references core.MessageBoard(Id),
	Text varchar(max) NOT NULL,
	CONSTRAINT messages_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.MessageAttribution (
	Id uniqueidentifier NOT NULL,
	MessageId uniqueidentifier NOT NULL references core.MessageBoard(Id),
	GuestId uniqueidentifier NOT NULL references core.Guests(Id),
	CONSTRAINT messageAttribution_pkey PRIMARY KEY (Id)
)