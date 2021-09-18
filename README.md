# FSharpDataBot
Twitter robot that outputs Graphs and Statistics from the World Bank Data

![Travis](https://travis-ci.com/vhogemann/FSharpDataBot.svg?branch=master)
![FsharpDataBot Log](doc/img/FSharpDataBotBanner.jpg)

## Usage

This bot is listening for any mention to the [@fsharp_data_bot](https://twitter.com/fsharp_data_bot)
on Twitter. To get a response from it, just send it a message in the format

> @fsharp_data_bot COUNTRIES INDICATORS START_YEAR END_YEAR

PARAM | Description
----- | ------ 
COUNTRIES | Any list of country names (no spaces) or their two or three ISO codes. Ex: BR, USA, Italy
INDICATORS | Any of the indicators listed bellow
START_YEAR | Four digit year. Ex.: 1997, 2008
END_YEAR | Four digit year. Ex.: 1997, 2008

Examples:

To plot the GDP from 2010 to 2020 for Brazil, Italy and the United States do:

> @fsharp_data_bot brazil italy usa gdp 2010 2020

![Example Reply](doc/img/Screenshot%202020-05-27%20at%2016.32.32.png)

### World Bank Indicators

These indicators fetch data from the [World Bank Open Data](https://data.worldbank.org/)
API:

Indicator | Description
--------- | -----------
gdp | Gross Domestic Product in USD
gdp/capita | Gross Domestic Product per Capita in USD
gdp/growth | Gross Domestic Product annual growth %
literacy | Literacy rate, youth total (% of people ages 15-24)
literacy/young | Same as above
literacy/adult | Literacy rate, adult total (% of people ages 15 and above)
employment | Employment to population ratio, 15+, total (%) (national estimate)
unemployment | Unemployment, total (% of total labor force) (modeled ILO estimate)
electricity-access | Access to electricity (% of population)
exchange-rate/usd | Official exchange rate (LCU per US$, period average)
poverty-gap | Poverty headcount ratio at national poverty lines (% of population)
stock-market | Stocks traded, total value (current US$)
external-balance | External balance on goods and services (current US$)

### Covid19Api Indicators

Indicators using the [Covid19 Api](https://covid19api.com/):

Indicator | Description
--------- | -----------
covid | Coronavirus confirmed cases, recoveries and deaths since day 1