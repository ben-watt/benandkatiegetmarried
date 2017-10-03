

DECLARE @eventId uniqueidentifier = '50a3e288-4332-40b3-aff7-8f5db6e8783d'
, @benId uniqueidentifier = '334d6f0a-27d1-4655-bde7-bc75926dbb1e'
, @katieId uniqueidentifier = '8a5b0640-cd1a-4f66-b334-b1d4a8cb921f'

BEGIN 

INSERT INTO core.Events (Id, Name, Type, StartTime, EndTime) VALUES
(@eventId, 'Ben & Katie Get Married', 'Wedding', '2017-02-24 12:00:00', '2017-02-25 00:00:00');

INSERT INTO core.Weddings (eventId, Bride, Groom) VALUES
(@eventId, 'Katie', 'Ben');

INSERT INTO core.Users (Id, UserName, Password) VALUES 
(@benId, 'ben', '$2a$10$dBGckjkQD9Cayr65EITvo.ksOr07d7YA97AYCvMHd..4ubFkZLEx.')
, (@katieId, 'katie', '$2a$10$dBGckjkQD9Cayr65EITvo.ksOr07d7YA97AYCvMHd..4ubFkZLEx.');

INSERT INTO core.UserEventMapping (Id, UserId, eventId) VALUES
(NEWID(), @benId, @eventId)
, (NEWID(), @katieId, @eventId);

INSERT INTO core.Invites (Id, eventId, SecurityCode, Password, Greeting, Type) VALUES
(NEWID(), @eventId, 'ben', '$2a$10$dBGckjkQD9Cayr65EITvo.ksOr07d7YA97AYCvMHd..4ubFkZLEx.', null, 'Day');

INSERT INTO core.Venues (Id, eventId, Name, AddressLine1, AddressLine2, Town, County, PostCode, Country, Telephone, Website) VALUES
(NEWID(),
@eventId, 
'Worsley Park Marriott Hotel & Country Club',
'Worsley Park',
'Worsley',
'Manchester',
'Lancashire',
'M28 2QT',
'UK',
'0161 975 2000',
'http://www.marriott.com/hotels/travel/mangs-worsley-park-marriott-hotel-and-country-club/?scid=bb1a189a-fec3-4d19-a255-54ba596febe2');

END