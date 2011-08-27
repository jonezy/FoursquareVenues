# How to use

Using this is super simple, the whole thing is built on dynamics so all you do is pass in a venue endpoint populated
with data and you'll receive a dynamic object back.

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