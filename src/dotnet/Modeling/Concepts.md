# And Then...

## Summary

The idea of this project is to create a framework for low code/no code development
 
## Features

* User (personal) Profile
  * name: first, middle, last, prefix, suffix
  * email address (additional are under contacts)
  * associated authentications
* Person Contact Management
  * types 
    * formatter 
    * validators
    * presenters
* User Authentication 
  * Account Creation
  * Password Aging
  * Password Changing
  * Multifactor Authentication 
  * User registration tokens
* User Authorization 
  * Permissions
  * Management
    * Role Composition 
    * Module Rights Enumeration
    * User Role Assignment
* User Claims
  * Definable by module
  * roles will be mapped as "named role"
  * rights will be mapped at "module::feature::right"
* User Navigation / sitemap 
  * Filtered by claims
  * Extensible by Module
  * Supports tagging, targeting, breadcrumbs
    * tagging allows for filtered views of navigation to present options on select menus and depths
    * targeting is stateful navigation for presenting child menu options 
    * support application wide deemed linking
    * targeted bread crumbs should support optional drop-down navigation
  * users map override ordering, presentation, grouping
  * navigation shall be handled by navigation request events
* Client Event Bus
  * central even pipeline to application events
  * handlers may be defined and matched by user
* Service Event Bus
  * Central business event source
  * events addressed to users may be routed to client if user is connected (notifications)
* User Notifications 
  * banners 
  * toast
  * directed messages
  * history
* User Messages
  * Directed (in-application)
  * Directed (external)
  * Templated
  * Triggered
* Data Enhancement 
  * chained pipeline 
  * may be global (always enhance) or contextual (rule based)
* User Management
  * global admin
  * group admin (filters on claim)
    * groups are rule based 
    * rules are based on claims 
* Data Persistence Pattern
  * model validation (400 on fail)
  * data authorization check (401 on fail)
    * if not found error in response
  * culture conversation 
  * relations may be by surrogate or key?
    * if unmapped warn in response 
    * surrogate may be
      * pk
      * relation::pk
      * code
      * relation::pk
  * partial updates
    * if property is null or not defined it will be ignore and not changed
    * if property update is not authorized it will be ignored and warned 
    * should child sets be fully inclusive or should they be change events?
  * persistent 
    * insert if no key
    * update if key, if key not found error (409 or 410?)
  * response model
    * same model will be returned but status code may be different based on step failed
    * returned model will be current persistent model not requested model
  * deletion 
    * instead of returning model will return true or false.  True means actually deleted, false means doesn't exist... may still return 401 if user doesn't have permissions 
* Data Query Pattern
  * use new filter model
  * should we have request depth?
* Data Schema Definition
  * ???

## Data Modeling and Persistence

*May need to define a model for controlling mapping from schema definition to requested persistence model*

### Modeling 

Per entity type there would be a conconical definition.  This model describes how data is to be presented to the user.

Type definitions may include things such as masking, data authorization, pattern checking 

### Persistence

* Referened objects may be linked by Key, Reference::Key, Code, or Reference::Code
* Invalid property values will be ignored for change and returned as warning in the response
* Undefined properties will be ignored for change and returned as a warning in the response

### PUT

If using `HTTP PUT` the object will be expected to be fully defined the entity will be compared to the existing model and converted into a change request based on differences.  Null Child collections will still be ignored but if they are empty sets they will be cleared

### Patch

This will be a list of change commands to be applied.  Changes will be defined as follows

```json
[
    {
        'source': "XYZ",
        'value': "XYZ",
        'action': "+|-"
    },
    ...
]
```

## Schema Definitions

* https://json-schema.org/
* XSD 1.0 (XSD 1.1 would require additional tooling)

### Example

#### Model Definition

```
Person
    Prefix : string?
    FirstName : name?
    LastName : name,
    Suffix : string?
    
    EmailAddress : email
    EmailAddressVerified : verify
    Logins : AuthenticationReference[+] 

    @expose : authenticated
    @authorize :           
        @read : [authenticated]
        @write: [self, admin, system]

AuthenticationReference
    ProviderName : string <<code>>
    ObjectId : string <<code>>
    AuthenticationVerified : verify

email : string
     // https://www.rfc-editor.org/rfc/rfc5322 ??
     
verify : bool
    @authorize :
        true : [system, admin]
        false : [self, admin, system]

name : string
    @validation:
        regex: [^\s] //no whitespace
        
```

https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/interpolated-string-handler
https://learn.microsoft.com/en-us/dotnet/standard/base-types/grouping-constructs-in-regular-expressions

how should we handle masking

maybe regex with grouping 


##### Modeling Rules

* Rules to sets of values
  * [+] or [1-] means must have at least 1
  * [], [-] or [*] would allow 0 to many
  * [x-y] would be a bounded set
    * [5-] would mean 5 or more
    * [-5] would be up to 5
    * [5-10] would be there must be at least 5 but no more than 10 entries
* values starting with `@` are rules and not properties
  * `@authorize` are rules that limit who may change a value
    * on top level model this will cause a `401 unauthorized` and not apply any changes
    * on properties 
      * if `@enforce` is `undefined` or `false` these will post a warning to result model and exclude the change from being applied
      * if `@enforce` is `true` these will cause a `401 unauthorized` and not apply any changes
    * if the `@authorize` is on an object then the match rule will match property names
    * if the `@authorize` is on a value then the match rule will pattern match the value
    * the set of values for the match rule are
      * `everyone` any authenticated or unauthenticated user
      * `authenticated` any authenticated user
      * `self` current user 
      * `admin` user with system admin claim
      * `system` a backend processor
      * `'claim key$$value match'` the name of a particular targeted claim and its required value
  * `@expose` defines a data service endpoint
    * `everyone` creates an externally published endpoint that does not require authentication
    * `authenticated` creates an externally published endpoint that requires authentication
    * `internal` creates an internally published endpoint for cross system communication
    * if this is not provided or an other value end endpoint is created
* properties that start with `_` 
  * are readonly, will always be ignored from the change list
  * they may be mapped value so you might be able to change the value though the directed properties
  * warnings will be posted to the result messages if change is requested to readonly property it will be ignored



#### GET /person/1234

```json
{
    '_id' : 1234
    'FirstName': 'Matthew',
    'LastName': 'Whited',
    'EmailAddress': 'mwwhited@hotmail.com',
    'Logins' : [
        {
            '_id' : 4321
            '_code': 'B2C-lightwellnucleus$$02434500-fcc4-4249-8c6d-156a74ac9560',
            'ProviderName': 'B2C-lightwellnucleus',
            'ObjectId': '02434500-fcc4-4249-8c6d-156a74ac9560'
        },        
        {
            '_id' : 4322
            '_code': 'Github-XYZ$$704aef27-f57e-4b81-a952-3313442d6086',
            'ProviderName': 'Github-XYZ',
            'ObjectId': '704aef27-f57e-4b81-a952-3313442d6086'
        },        
        {
            '_id' : 4323
            '_code': 'Facebook-123$$97c1a033-17de-429c-a58d-0c822b29abac',
            'ProviderName': 'Facebook-123',
            'ObjectId': '97c1a033-17de-429c-a58d-0c822b29abac'
        }
    ]
}
```

#### POST /data/person/1234

if the `_id` is not provided on the object but is included on the URL it will be applied to the request before processing.

if the property name is prefixed with `$` it will be mapped to a change request.  if directly mapped as an array and it no null it will be assumed to be inclusive to output and changes will be generated by the differences

if values are null they will be ignored for the change request

##### Request

```json
{
    'lastName': 'changeme',
    '$logins': [
       {'action': '+', 'source':'_code', 'value': 'Amazon-876$$fd40e213-e97c-41b2-9603-73133b6bbfd0'},
       {'action': '-', 'source':'_code',  'value': 'Github-XYZ$$704aef27-f57e-4b81-a952-3313442d6086'},
       {'action': '-', 'source':'_code', 'value': 'Github-XYZ$$704aef27-f57e-4b81-a952-3313442d6087'}, //warn not found
       {'action': '-', 'source':'_id',  'value': '4323'},
    ]
}
```

##### Response 200
```json
{
    '_id' : 1234
    'FirstName': 'Matthew',
    'LastName': 'changeme',
    'EmailAddress': 'mwwhited@hotmail.com',
    'Logins' : [
        {
            '_id' : 4321
            '_code': 'B2C-lightwellnucleus$$02434500-fcc4-4249-8c6d-156a74ac9560',
            'ProviderName': 'B2C-lightwellnucleus',
            'ObjectId': '02434500-fcc4-4249-8c6d-156a74ac9560'
        },       
        {
            '_id' : 5231
            '_code': 'Amazon-876$$fd40e213-e97c-41b2-9603-73133b6bbfd0',
            'ProviderName': 'Amazon-876',
            'ObjectId': 'fd40e213-e97c-41b2-9603-73133b6bbfd0'
        }
    ],
    '_mesages': [
        {'level':warning', 'messageCode': 'PERSON::LOGIN::NOT_FOUND', 'context': '_code', 'metadata': { 'value': 'Github-XYZ$$704aef27-f57e-4b81-a952-3313442d6087' }}
    ]
}
```

#### POST /data/person/1234

if the key from the URL does not match the `$id` on the request an bad request error will be returned with a `PERSON::KEY_MISMATCH` error

##### Request

```json
{
    '_id': 5432,
    ...
}
```

##### Response 400

```json
{
    '_id': 5432,
    '_mesages': [
        {'level':error', 'messageCode': 'PERSON::KEY_MISMATCH', 'context': '_id', 'metadata': { 'url':'/data/person/1234', 'route__key': 1234, '_id': 5432}}
    ]
}
```