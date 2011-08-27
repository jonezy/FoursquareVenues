# How to use

Using this is super simple, the whole thing is built on dynamics so all you do is pass in a venue endpoint populated
with data and you'll receive a dynamic object back.

You get back an objec that has 2 nodes.  meta and response.  Meta is the http code associated with the request (200, 400 etc). Response is 
a collection of venue objects with properties dynamically filled (see the properties here https://developer.foursquare.com/docs/responses/venue.html)

	string clientId = "YourClientId";
	string clientSecret = "YourSecret";
	string latLong = "43.6,-79.3"; // this is toronto
	string version = DateTime.Today.ToString("yyyyMMdd");
	string query = ""; // venue to query for
	
	string searchRequest = string.Format("https://api.foursquare.com/v2/venues/search?ll={0}&query={1}&client_id={2}&client_secret={3}&v={4}",
		latLong,
		query,
		clientId,
		clientSecret,
		version);
	
	var client = new FoursquareVenueClient();
	var venues = client.Execute(searchRequest);
	
	Console.WriteLine(venues.meta);
	foreach (var item in venues.response) {
		Console.WriteLine(item.id);
	}
	
	Console.ReadLine();