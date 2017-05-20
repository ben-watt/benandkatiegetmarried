
CREATE EXTENSION "uuid-ossp";

DO $$

DECLARE eventId uuid = uuid_generate_v4();

BEGIN 

INSERT INTO core.Events ("Id", "EventName", "EventType", "StartTime", "EndTime") VALUES
(eventId, 'Ben & Katie Get Married', 'Wedding', '2017-02-24 12:00:00', '2017-02-24 24:00:00');

INSERT INTO core.Weddings ("EventId", "Bride", "Groom") VALUES
(eventId, 'Katie', 'Ben');

END $$