CREATE VIEW [core].[Responses] AS
SELECT g.InviteId,
g.FirstName,
g.LastName,
rr.Response,
rr.MealChoice,
r.SongLink,
r.DietaryRequirements
FROM core.RsvpResponses AS rr
    INNER JOIN core.Rsvps AS r
	   ON r.Id = rr.RsvpId
    INNER JOIN core.Guests AS g
	   ON g.Id = rr.GuestId