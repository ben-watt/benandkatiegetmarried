
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

CREATE TABLE core.Rsvps
(
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	InviteId uniqueidentifier NOT NULL references core.Invites(Id),
	ResponseMessage varchar(max) NULL,
	DietaryRequirements character varying (500) NULL,
	SongLink character varying (500) NULL,
	CONSTRAINT Rsvp_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Meals 
(
	Id uniqueidentifier NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	Name character varying (300) NOT NULL,
	Course character varying (200) NULL,
	MealGroup character varying (200) NULL,
	CONSTRAINT meals_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.RsvpResponses
(
	Id uniqueidentifier NOT NULL,
	RsvpId uniqueidentifier NOT NULL,
	GuestId uniqueidentifier NOT NULL references core.Guests(Id),
	Response bit DEFAULT NULL,
	MealChoice nvarchar(500) NULL,
	CONSTRAINT RsvpResponse_pkey PRIMARY KEY (Id),
	CONSTRAINT RsvpResponse_uniqueGuestId UNIQUE (GuestId)
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

CREATE TABLE core.MessageBoards (
	Id uniqueidentifier NOT NULL,
	[Type] varchar(200) NOT NULL,
	EventId uniqueidentifier NOT NULL references core.Events(Id),
	[Name] character varying (200) NOT NULL,
	CONSTRAINT messageBoard_pkey PRIMARY KEY (Id),
	CONSTRAINT messageBoard_event_type_unique UNIQUE ([EventId], [Type])
)

CREATE TABLE core.Messages (
	Id uniqueidentifier NOT NULL,
	MessageBoardId uniqueidentifier NOT NULL references core.MessageBoards(Id),
	[Text] nvarchar(max) NOT NULL,
	[Date] datetime2 NOT NULL DEFAULT GETUTCDATE(),
	[Hierarchy] hierarchyId NOT NULL,
	CONSTRAINT messages_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Likes (
	Id uniqueidentifier NOT NULL,
	MessageId uniqueidentifier NOT NULL references core.Messages(Id),
	GuestId uniqueidentifier NOT NULL references core.Guests(Id),
	CONSTRAINT likes_pkey PRIMARY KEY (Id)
)
				  
CREATE TABLE core.MessageAttributions (
	Id uniqueidentifier NOT NULL,
	MessageId uniqueidentifier NOT NULL references core.Messages(Id),
	GuestId uniqueidentifier NOT NULL references core.Guests(Id),
	CONSTRAINT messageAttribution_pkey PRIMARY KEY (Id)
)