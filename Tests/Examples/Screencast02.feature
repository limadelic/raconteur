Feature: v0.2 Screencast
	Description

Scenario: Retweet

	Given a tweet by "limadelic" with content "check out Raconteur!"
	When I retweet it
	Then the update text should be "RT @limadelic check out Raconteur!"

Scenario:  Help File Is Product Specific

	Load Products
	The Help File should be "Products.pdf"
	Select "Webcams"
	The Help File should be "Webcams.pdf"

Scenario: Response Header Parser

	Given the Response
	"
		HTTP/1.1 200 OK
		Content-Length: 267
		Content-Type: application/xml
		Date: Wed, 19 Nov 2008 21:48:10 GMT    
	"

	The Header should have
	[ Code | Lenght ]
	|  200 |    267 |

Scenario: Scalar Multiplication of Matrices

	M =
		| 1| 2|
		| 0|-1|

	2 * M =
		| 2| 4|
		| 0|-2|

Scenario: Supporting multiple services to shorten urls

	Select url shortener "service"
	Post a status update "http://raconteur.github.com/"
	Verify the urls was "shorten"

	Examples:
	| service | shorten					   | 
	| is.gd   | http://is.gd/gLEoH         |
	| bit.ly  | http://bit.ly/cTLoh7       |
	| tinyurl | http://tinyurl.com/3xj9z7t |
