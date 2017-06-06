
CREATE SCHEMA core

CREATE TABLE core.Events 
(
    Id uuid NOT NULL,
    Name character varying(500),
	Type character varying(200),
    StartTime timestamp,
    EndTime timestamp,
	CONSTRAINT events_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Weddings
(
    EventId uuid NOT NULL references core.Events(Id),
    Bride character varying(200) NOT NULL,
    Groom character varying(200) NOT NULL,
    CONSTRAINT weddings_pkey PRIMARY KEY (EventId)
)

CREATE TABLE core.Venues 
(
	Id uuid NOT NULL,
	EventId uuid NOT NULL references core.Events(Id),
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
	Id uuid NOT NULL,
	EventId uuid NOT NULL references core.Events(Id),
	SecurityCode citext NOT NULL, 
	Password text NOT NULL,
	Greeting character varying (500) NULL,
	LoginAttempts int DEFAULT 0,
	CONSTRAINT invites_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Guests
(
	Id uuid NOT NULL,
	EventId uuid NOT NULL references core.Events(Id),
	InviteId uuid NOT NULL references core.Invites(Id),
	FirstName character varying (200) NOT NULL,
	LastName character varying (200) NOT NULL,
	UserName character varying NULL,
	Type character varying(200) NOT NULL,
	IsFeatured bool default false NOT NULL,
	HasSentRsvp bool default false NOT NULL,
	CONSTRAINT guests_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.RSVPs
(
	Id uuid NOT NULL,
	EventId uuid NOT NULL references core.Events(Id),
	InviteId uuid NOT NULL references core.Invites(Id),
	GuestId uuid NOT NULL references core.Guests(Id),
	Response bool DEFAULT NULL,
	ResponseMessage text NULL,
	DietaryRequirements character varying (500) NULL,
	SongLink character varying (500) NULL,
	CONSTRAINT RSVP_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Meals (
	Id uuid NOT NULL,
	EventId uuid NOT NULL references core.Events(Id),
	Name character varying (300) NOT NULL,
	Course character varying (200) NULL,
	MealGroup character varying (200) NULL,
	CONSTRAINT meals_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.MealChoises 
(
	Id uuid NOT NULL,
    EventId uuid NOT NULL references core.Events(Id),
	GuestId uuid NOT NULL references core.Guests(Id),
	RSVPId uuid NOT NULL references core.RSVPs(Id),
	MealId uuid NOT NULL references core.Meals(Id),
	CONSTRAINT mealChoises_pKey PRIMARY KEY (Id)
)

CREATE TABLE core.Users (
	Id uuid NOT NULL,
	UserName citext NOT NULL,
	Password text NOT NULL,
    LoginAttempts int DEFAULT 0,
	CONSTRAINT users_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.UserEventMapping (
	Id uuid NOT NULL,
	UserId uuid NOT NULL references core.Users(Id),
	EventId uuid NOT NULL references core.Events(Id),
	CONSTRAINT userEventMapping_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.MessageBoard (
	Id uuid NOT NULL,
	EventId uuid NOT NULL references core.Events(Id),
	Name character varying (200) NOT NULL,
	CONSTRAINT messageBoard_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.Messages (
	Id uuid NOT NULL,
	MessageBoardId uuid NOT NULL references core.MessageBoard(Id),
	Text text NOT NULL,
	CONSTRAINT messages_pkey PRIMARY KEY (Id)
)

CREATE TABLE core.MessageAttribution (
	Id uuid NOT NULL,
	MessageId uuid NOT NULL references core.MessageBoard(Id),
	GuestId uuid NOT NULL references core.Guests(Id),
	CONSTRAINT messageAttribution_pkey PRIMARY KEY (Id)
)