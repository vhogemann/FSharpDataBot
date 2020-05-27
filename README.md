# FSharpDataBot
Twitter robot that outputs Graphs and Statistics from the World Bank Data

![Travis](https://travis-ci.com/vhogemann/FSharpDataBot.svg?branch=master)
![FsharpDataBot Log](doc/img/FSharpDataBotBanner.jpg)

## Usage

### World Bank Indicators

These indicators fetch data from the [World Bank Open Data](https://data.worldbank.org/)
API:

Indicator | Description
--------- | -----------
gdp | Gross Domestic Product in USD
gdp/capita | Gross Domestic Product per Capita in USD
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