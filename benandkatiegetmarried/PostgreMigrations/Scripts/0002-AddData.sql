

DO $$
DECLARE 
eventId uuid = uuid_generate_v4();
benId uuid = uuid_generate_v4();
katieId uuid = uuid_generate_v4();

BEGIN 

INSERT INTO core.Events (Id, Name, Type, StartTime, EndTime) VALUES
(eventId, 'Ben & Katie Get Married', 'Wedding', '2017-02-24 12:00:00', '2017-02-24 24:00:00');

INSERT INTO core.Weddings (EventId, Bride, Groom) VALUES
(eventId, 'Katie', 'Ben');

INSERT INTO core.Users (Id, UserName, Password) VALUES 
(benId, 'Ben', 'test123')
, (katieId, 'Katie', 'test123');

INSERT INTO core.UserEventMapping (Id, UserId, EventId) VALUES
(uuid_generate_v4(), benId, eventId)
, (uuid_generate_v4(), katieId, eventId);

INSERT INTO core.Invites (Id, EventId, SecurityCode, Password, Greeting) VALUES
(uuid_generate_v4(), eventId, 'ben', 'test123', null);

END $$