module Countries

open NeoSmart.Unicode

type Country =
    { Name: string
      TwoLetterCode: string
      ThreeLetterCode: string
      Emoji: SingleEmoji }

let country name two three flag =
    { Name = name
      TwoLetterCode = two
      ThreeLetterCode = three
      Emoji = flag }

let List =
    [ country "afghanistan" "af" "afg" Emoji.Afghanistan
      country "albania" "al" "alb" Emoji.Albania
      country "algeria" "dz" "dza" Emoji.Algeria
      country "american samoa" "as" "asm" Emoji.Samoa
      country "andorra" "ad" "and" Emoji.Andorra
      country "angola" "ao" "ago" Emoji.Angola
      country "anguilla" "ai" "aia" Emoji.Anguilla
      country "antarctica" "aq" "ata" Emoji.WhiteFlag
      country "antigua and barbuda" "ag" "atg" Emoji.AntiguaAndBarbuda
      country "argentina" "ar" "arg" Emoji.Argentina
      country "armenia" "am" "arm" Emoji.Armenia
      country "aruba" "aw" "abw" Emoji.Aruba
      country "australia" "au" "aus" Emoji.Australia
      country "austria" "at" "aut" Emoji.Austria
      country "azerbaijan" "az" "aze" Emoji.Azerbaijan
      country "bahamas" "bs" "bhs" Emoji.Bahamas
      country "bahrain" "bh" "bhr" Emoji.Bahrain
      country "bangladesh" "bd" "bgd" Emoji.Bangladesh
      country "barbados" "bb" "brb" Emoji.Barbados
      country "belarus" "by" "blr" Emoji.Belarus
      country "belgium" "be" "bel" Emoji.Belgium
      country "belize" "bz" "blz" Emoji.Belize
      country "benin" "bj" "ben" Emoji.Benin
      country "bermuda" "bm" "bmu" Emoji.Bermuda
      country "bhutan" "bt" "btn" Emoji.Bhutan
      country "bolivia" "bo" "bol" Emoji.Bolivia
      country "bonaire" "bq" "bes" Emoji.WhiteFlag
      country "bosnia and herzegovina" "ba" "bih" Emoji.BosniaAndHerzegovina
      country "botswana" "bw" "bwa" Emoji.Botswana
      country "bouvet island" "bv" "bvt" Emoji.BouvetIsland
      country "brazil" "br" "bra" Emoji.Brazil
      country "british indian ocean territory" "io" "iot" Emoji.BritishIndianOceanTerritory
      country "brunei" "bn" "brn" Emoji.Brunei
      country "bulgaria" "bg" "bgr" Emoji.Bulgaria
      country "burkina faso" "bf" "bfa" Emoji.BurkinaFaso
      country "burundi" "bi" "bdi" Emoji.Burundi
      country "cambodia" "kh" "khm" Emoji.Cambodia
      country "cameroon" "cm" "cmr" Emoji.Cameroon
      country "canada" "ca" "can" Emoji.Canada
      country "cape verde" "cv" "cpv" Emoji.CapeVerde
      country "cayman islands" "ky" "cym" Emoji.CaymanIslands
      country "central african republic" "cf" "caf" Emoji.CentralAfricanRepublic
      country "chad" "td" "tcd" Emoji.Chad
      country "chile" "cl" "chl" Emoji.Chile
      country "china" "cn" "chn" Emoji.China
      country "christmas island" "cx" "cxr" Emoji.ChristmasIsland
      country "cocos islands" "cc" "cck" Emoji.CocosKeelingIslands
      country "keeling islands" "cc" "cck" Emoji.CocosKeelingIslands
      country "colombia" "co" "col" Emoji.Colombia
      country "comoros" "km" "com" Emoji.Comoros
      country "congo" "cg" "cog" Emoji.WhiteFlag
      country "democratic republic of the congo" "cd" "cod" Emoji.WhiteFlag
      country "cook islands" "ck" "cok" Emoji.CookIslands
      country "costa rica" "cr" "cri" Emoji.CostaRica
      country "croatia" "hr" "hrv" Emoji.Croatia
      country "cuba" "cu" "cub" Emoji.Cuba
      country "curacao" "cw" "cuw" Emoji.Curaçao
      country "cyprus" "cy" "cyp" Emoji.Cyprus
      country "czech republic" "cz" "cze" Emoji.Czechia
      country "cote d'ivoire" "ci" "civ" Emoji.CôteDivoire
      country "denmark" "dk" "dnk" Emoji.Denmark
      country "djibouti" "dj" "dji" Emoji.Djibouti
      country "dominica" "dm" "dma" Emoji.Dominica
      country "dominican republic" "do" "dom" Emoji.DominicanRepublic
      country "ecuador" "ec" "ecu" Emoji.Ecuador
      country "egypt" "eg" "egy" Emoji.Egypt
      country "el salvador" "sv" "slv" Emoji.ElSalvador
      country "equatorial guinea" "gq" "gnq" Emoji.EquatorialGuinea
      country "eritrea" "er" "eri" Emoji.Eritrea
      country "estonia" "ee" "est" Emoji.Estonia
      country "ethiopia" "et" "eth" Emoji.Ethiopia
      country "falkland" "fk" "flk" Emoji.FalklandIslands
      country "maldivas" "fk" "flk" Emoji.FalklandIslands
      country "faroe islands" "fo" "fro" Emoji.FaroeIslands
      country "fiji" "fj" "fji" Emoji.Fiji
      country "finland" "fi" "fin" Emoji.Finland
      country "france" "fr" "fra" Emoji.France
      country "french guiana" "gf" "guf" Emoji.FrenchGuiana
      country "french polynesia" "pf" "pyf" Emoji.FrenchPolynesia
      country "french southern territories" "tf" "atf" Emoji.FrenchSouthernTerritories
      country "gabon" "ga" "gab" Emoji.Gabon
      country "gambia" "gm" "gmb" Emoji.Gambia
      country "georgia" "ge" "geo" Emoji.Georgia
      country "germany" "de" "deu" Emoji.Germany
      country "ghana" "gh" "gha" Emoji.Ghana
      country "gibraltar" "gi" "gib" Emoji.Gibraltar
      country "greece" "gr" "grc" Emoji.Greece
      country "greenland" "gl" "grl" Emoji.Greenland
      country "grenada" "gd" "grd" Emoji.Grenada
      country "guadeloupe" "gp" "glp" Emoji.Guadeloupe
      country "guam" "gu" "gum" Emoji.Guam
      country "guatemala" "gt" "gtm" Emoji.Guatemala
      country "guernsey" "gg" "ggy" Emoji.Guernsey
      country "guinea" "gn" "gin" Emoji.Guinea
      country "guinea-bissau" "gw" "gnb" Emoji.GuineaBissau
      country "guyana" "gy" "guy" Emoji.Guyana
      country "haiti" "ht" "hti" Emoji.Haiti
      country "heard island and mcdonald islands" "hm" "hmd" Emoji.HeardAndMcdonaldIslands
      country "vatican" "va" "vat" Emoji.VaticanCity
      country "honduras" "hn" "hnd" Emoji.Honduras
      country "hong kong" "hk" "hkg" Emoji.HongKongSarChina
      country "hungary" "hu" "hun" Emoji.Hungary
      country "iceland" "is" "isl" Emoji.Iceland
      country "india" "in" "ind" Emoji.India
      country "indonesia" "id" "idn" Emoji.Indonesia
      country "iran" "ir" "irn" Emoji.Iran
      country "iraq" "iq" "irq" Emoji.Iraq
      country "ireland" "ie" "irl" Emoji.Ireland
      country "isle of man" "im" "imn" Emoji.IsleOfMan
      country "israel" "il" "isr" Emoji.Israel
      country "italy" "it" "ita" Emoji.Italy
      country "jamaica" "jm" "jam" Emoji.Jamaica
      country "japan" "jp" "jpn" Emoji.Japan
      country "jersey" "je" "jey" Emoji.Jersey
      country "jordan" "jo" "jor" Emoji.Jordan
      country "kazakhstan" "kz" "kaz" Emoji.Kazakhstan
      country "kenya" "ke" "ken" Emoji.Kenya
      country "kiribati" "ki" "kir" Emoji.Kiribati
      country "north korea" "kp" "prk" Emoji.NorthKorea
      country "south korea" "kr" "kor" Emoji.SouthKorea
      country "kuwait" "kw" "kwt" Emoji.Kuwait
      country "kyrgyzstan" "kg" "kgz" Emoji.Kyrgyzstan
      country "laos" "la" "lao" Emoji.Laos
      country "latvia" "lv" "lva" Emoji.Latvia
      country "lebanon" "lb" "lbn" Emoji.Lebanon
      country "lesotho" "ls" "lso" Emoji.Lesotho
      country "liberia" "lr" "lbr" Emoji.Liberia
      country "libya" "ly" "lby" Emoji.Libya
      country "liechtenstein" "li" "lie" Emoji.Liechtenstein
      country "lithuania" "lt" "ltu" Emoji.Lithuania
      country "luxembourg" "lu" "lux" Emoji.Luxembourg
      country "macao" "mo" "mac" Emoji.MacauSarChina
      country "macedonia" "mk" "mkd" Emoji.Macedonia
      country "madagascar" "mg" "mdg" Emoji.Madagascar
      country "malawi" "mw" "mwi" Emoji.Malawi
      country "malaysia" "my" "mys" Emoji.Malaysia
      country "maldives" "mv" "mdv" Emoji.Maldives
      country "mali" "ml" "mli" Emoji.Mali
      country "malta" "mt" "mlt" Emoji.Malta
      country "marshall islands" "mh" "mhl" Emoji.MarshallIslands
      country "martinique" "mq" "mtq" Emoji.Martinique
      country "mauritania" "mr" "mrt" Emoji.Mauritania
      country "mauritius" "mu" "mus" Emoji.Mauritius
      country "mayotte" "yt" "myt" Emoji.Mayotte
      country "mexico" "mx" "mex" Emoji.Mexico
      country "micronesia" "fm" "fsm" Emoji.Micronesia
      country "moldova" "md" "mda" Emoji.Moldova
      country "monaco" "mc" "mco" Emoji.Monaco
      country "mongolia" "mn" "mng" Emoji.Mongolia
      country "montenegro" "me" "mne" Emoji.Montenegro
      country "montserrat" "ms" "msr" Emoji.Montserrat
      country "morocco" "ma" "mar" Emoji.Morocco
      country "mozambique" "mz" "moz" Emoji.Mozambique
      country "myanmar" "mm" "mmr" Emoji.MyanmarBurma
      country "namibia" "na" "nam" Emoji.Namibia
      country "nauru" "nr" "nru" Emoji.Nauru
      country "nepal" "np" "npl" Emoji.Nepal
      country "netherlands" "nl" "nld" Emoji.Netherlands
      country "new caledonia" "nc" "ncl" Emoji.NewCaledonia
      country "new zealand" "nz" "nzl" Emoji.NewZealand
      country "nicaragua" "ni" "nic" Emoji.Nicaragua
      country "niger" "ne" "ner" Emoji.Niger
      country "nigeria" "ng" "nga" Emoji.Nigeria
      country "niue" "nu" "niu" Emoji.Niue
      country "norfolk island" "nf" "nfk" Emoji.NorfolkIsland
      country "mariana islands" "mp" "mnp" Emoji.NorthernMarianaIslands
      country "norway" "no" "nor" Emoji.Norway
      country "oman" "om" "omn" Emoji.Oman
      country "pakistan" "pk" "pak" Emoji.Pakistan
      country "palau" "pw" "plw" Emoji.Palau
      country "palestine" "ps" "pse" Emoji.PalestinianTerritories
      country "panama" "pa" "pan" Emoji.Panama
      country "papua new guinea" "pg" "png" Emoji.PapuaNewGuinea
      country "paraguay" "py" "pry" Emoji.Paraguay
      country "peru" "pe" "per" Emoji.Peru
      country "philippines" "ph" "phl" Emoji.Philippines
      country "pitcairn" "pn" "pcn" Emoji.PitcairnIslands
      country "poland" "pl" "pol" Emoji.Poland
      country "portugal" "pt" "prt" Emoji.Portugal
      country "puerto rico" "pr" "pri" Emoji.PuertoRico
      country "qatar" "qa" "qat" Emoji.Qatar
      country "romania" "ro" "rou" Emoji.Romania
      country "russia" "ru" "rus" Emoji.Russia
      country "rwanda" "rw" "rwa" Emoji.Rwanda
      country "reunion" "re" "reu" Emoji.WhiteFlag
      country "saint barthelemy" "bl" "blm" Emoji.StBarthélemy
      country "saint helena" "sh" "shn" Emoji.StHelena
      country "saint kitts and nevis" "kn" "kna" Emoji.StKittsAndNevis
      country "saint lucia" "lc" "lca" Emoji.StLucia
      country "saint martin" "mf" "maf" Emoji.StMartin
      country "saint pierre and miquelon" "pm" "spm" Emoji.StPierreAndMiquelon
      country "saint vincent and the grenadines" "vc" "vct" Emoji.StVincentAndGrenadines
      country "samoa" "ws" "wsm" Emoji.Samoa
      country "san marino" "sm" "smr" Emoji.SanMarino
      country "sao tome and principe" "st" "stp" Emoji.SãoToméAndPríncipe
      country "saudi arabia" "sa" "sau" Emoji.SaudiArabia
      country "senegal" "sn" "sen" Emoji.Senegal
      country "serbia" "rs" "srb" Emoji.Serbia
      country "seychelles" "sc" "syc" Emoji.Seychelles
      country "sierra leone" "sl" "sle" Emoji.SierraLeone
      country "singapore" "sg" "sgp" Emoji.Singapore
      country "saint maarten" "sx" "sxm" Emoji.SintMaarten
      country "slovakia" "sk" "svk" Emoji.Slovakia
      country "slovenia" "si" "svn" Emoji.Slovenia
      country "solomon islands" "sb" "slb" Emoji.SolomonIslands
      country "somalia" "so" "som" Emoji.Somalia
      country "south africa" "za" "zaf" Emoji.SouthAfrica
      country "south georgia and the south sandwich islands" "gs" "sgs" Emoji.SouthGeorgiaAndSouthSandwichIslands
      country "south sudan" "ss" "ssd" Emoji.SouthSudan
      country "spain" "es" "esp" Emoji.Spain
      country "sri lanka" "lk" "lka" Emoji.SriLanka
      country "sudan" "sd" "sdn" Emoji.Sudan
      country "suriname" "sr" "sur" Emoji.Suriname
      country "svalbard and jan mayen" "sj" "sjm" Emoji.SvalbardAndJanMayen
      country "swaziland" "sz" "swz" Emoji.Swaziland
      country "sweden" "se" "swe" Emoji.Sweden
      country "switzerland" "ch" "che" Emoji.Switzerland
      country "syria" "sy" "syr" Emoji.Syria
      country "taiwan" "tw" "twn" Emoji.Taiwan
      country "tajikistan" "tj" "tjk" Emoji.Tajikistan
      country "tanzania" "tz" "tza" Emoji.Tanzania
      country "thailand" "th" "tha" Emoji.Thailand
      country "timor-leste" "tl" "tls" Emoji.TimorLeste
      country "togo" "tg" "tgo" Emoji.Togo
      country "tokelau" "tk" "tkl" Emoji.Tokelau
      country "tonga" "to" "ton" Emoji.Tonga
      country "trinidad and tobago" "tt" "tto" Emoji.TrinidadAndTobago
      country "tunisia" "tn" "tun" Emoji.Tunisia
      country "turkey" "tr" "tur" Emoji.Turkey
      country "turkmenistan" "tm" "tkm" Emoji.Turkmenistan
      country "turks and caicos islands" "tc" "tca" Emoji.TurksAndCaicosIslands
      country "tuvalu" "tv" "tuv" Emoji.Tuvalu
      country "uganda" "ug" "uga" Emoji.Uganda
      country "ukraine" "ua" "ukr" Emoji.Ukraine
      country "united arab emirates" "ae" "are" Emoji.UnitedArabEmirates
      country "united kingdom" "gb" "gbr" Emoji.UnitedKingdom
      country "united states" "us" "usa" Emoji.UnitedStates
      country "united states minor outlying islands" "um" "umi" Emoji.UnitedStates
      country "uruguay" "uy" "ury" Emoji.Uruguay
      country "uzbekistan" "uz" "uzb" Emoji.Uzbekistan
      country "vanuatu" "vu" "vut" Emoji.Vanuatu
      country "venezuela" "ve" "ven" Emoji.Venezuela
      country "vietnam" "vn" "vnm" Emoji.Vietnam
      country "british virgin islands" "vg" "vgb" Emoji.BritishVirginIslands
      country "us virgin islands" "vi" "vir" Emoji.UsVirginIslands
      country "wallis and futuna" "wf" "wlf" Emoji.WallisAndFutuna
      country "western sahara" "eh" "esh" Emoji.WesternSahara
      country "yemen" "ye" "yem" Emoji.Yemen
      country "zambia" "zm" "zmb" Emoji.Zambia
      country "zimbabwe" "zw" "zwe" Emoji.Zimbabwe ]
