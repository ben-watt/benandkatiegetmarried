

DO $$

DECLARE eventId uuid = uuid_generate_v4();

BEGIN 

INSERT INTO core.Events (Id, Name, Type, StartTime, EndTime) VALUES
(eventId, 'Ben & Katie Get Married', 'Wedding', '2017-02-24 12:00:00', '2017-02-24 24:00:00');

INSERT INTO core.Weddings (EventId, Bride, Groom) VALUES
(eventId, 'Katie', 'Ben');

INSERT INTO core.Users (Id, UserName, Password) VALUES 
(uuid_generate_v4(), 'Ben', 'test123')
, (uuid_generate_v4(), 'Katie', 'test123');

END $$