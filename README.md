# Geico.RoadSideAssistance

API has 6 endpoints to support Roadside Assistance Service features

![image](https://user-images.githubusercontent.com/3716334/228678737-dda04bc9-32ae-4d41-bc51-f8ea4d3f5924.png)

Endpoint: POST **/RoadsideAssistanceService/assistants** <br/>
Purpose: This is to facilitate addition of new Assistant(s). System has couple of Assistants by default, but added this endpoint so testing would be easier. As soon as a new failicity is added, it would be available to Reserve. <br/>
Body: 
```
{
  "name": "<Assistant Name>",
  "geoLocation": {
    "latitude": 54.6907369,
    "longitude": -113.7649831
  }
}
```
<br/> <br/>
Endpoint: GET **/RoadsideAssistanceService/assistants/nearby?latitude=54.6907369&longitude=-113.7649831&limit=5** <br/>
Purpose: To get all Assistants near by User Location. **Latitude**, **Longitude** and **Limit** query parameters are to be provided

<br/> <br/>
Endpoint: POST **/RoadsideAssistanceService/assistants/reserve/nearby** <br/>
Purpose: To reserve a nearby available Assistant <br/>
Body:
```
{
  "customer": {
    "id": "<customer-id>",
    "name": "<customer-name>",
    "policyNumber": "<geico-insurance-policy-number>"
  },
  "customerLocation": {
    "latitude": 60.0269863,
    "longitude": -112.1345882
  }
}
```

<br/> <br/>
Endpoint: POST **/RoadsideAssistanceService/assistants/{id}/reserve** <br/>
Purpose: To reserve an avaiable Assistant, that User has found earlier using "nearby" endpoint <br/>
Query Param: **id**=**assistant-id-that-user-has-found-earlier** <br/>
Body:
```
{
  "id": "<customer-id>",
  "name": "<customer-name>",
  "policyNumber": "<geico-insurance-policy-number>"
}
```

<br/> <br/>
Endpoint: PATCH **/RoadsideAssistanceService/assistants/release** <br/>
Purpose: To Release an Assistant that was booked earlier <br/>
Body:
```
{
  "customer": {
    "id": "<customer-id>",
    "name": "<customer-name>",
    "policyNumber": "<geico-insurance-policy-number>"
  },
  "assistant": {
    "id": "<assistant-id>"
  }
}
```
<br/> <br/>

Endpoint: PATCH **/RoadsideAssistanceService/assistants/location** <br/>
Purpose: To Update location of an Assistant on: either post-releasing a booking or during the booking(to update location in the system) <br/>
Body:
```
{
  "assistant": {
    "id": "<assistant-id>"
  },
  "geoLocation": {
    "latitude": 36.2067818,
    "longitude": -113.746913
  }
}
```
<br/> <br/>
