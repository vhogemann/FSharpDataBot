module Countries

type Country =
    { Name: string
      TwoLetterCode: string
      ThreeLetterCode: string }

let country name two three =
    { Name = name
      TwoLetterCode = two
      ThreeLetterCode = three }

let List =
    [ country "afghanistan" "af" "afg"
      country "albania" "al" "alb"
      country "algeria" "dz" "dza"
      country "american samoa" "as" "asm"
      country "andorra" "ad" "and"
      country "angola" "ao" "ago"
      country "anguilla" "ai" "aia"
      country "antarctica" "aq" "ata"
      country "antigua and barbuda" "ag" "atg"
      country "argentina" "ar" "arg"
      country "armenia" "am" "arm"
      country "aruba" "aw" "abw"
      country "australia" "au" "aus"
      country "austria" "at" "aut"
      country "azerbaijan" "az" "aze"
      country "bahamas" "bs" "bhs"
      country "bahrain" "bh" "bhr"
      country "bangladesh" "bd" "bgd"
      country "barbados" "bb" "brb"
      country "belarus" "by" "blr"
      country "belgium" "be" "bel"
      country "belize" "bz" "blz"
      country "benin" "bj" "ben"
      country "bermuda" "bm" "bmu"
      country "bhutan" "bt" "btn"
      country "bolivia" "bo" "bol"
      country "bonaire" "bq" "bes"
      country "bosnia and herzegovina" "ba" "bih"
      country "botswana" "bw" "bwa"
      country "bouvet island" "bv" "bvt"
      country "brazil" "br" "bra"
      country "british indian ocean territory" "io" "iot"
      country "brunei darussalam" "bn" "brn"
      country "bulgaria" "bg" "bgr"
      country "burkina faso" "bf" "bfa"
      country "burundi" "bi" "bdi"
      country "cambodia" "kh" "khm"
      country "cameroon" "cm" "cmr"
      country "canada" "ca" "can"
      country "cape verde" "cv" "cpv"
      country "cayman islands" "ky" "cym"
      country "central african republic" "cf" "caf"
      country "chad" "td" "tcd"
      country "chile" "cl" "chl"
      country "china" "cn" "chn"
      country "christmas island" "cx" "cxr"
      country "cocos islands" "cc" "cck"
      country "keeling islands" "cc" "cck"
      country "colombia" "co" "col"
      country "comoros" "km" "com"
      country "congo" "cg" "cog"
      country "democratic republic of the congo" "cd" "cod"
      country "cook islands" "ck" "cok"
      country "costa rica" "cr" "cri"
      country "croatia" "hr" "hrv"
      country "cuba" "cu" "cub"
      country "curacao" "cw" "cuw"
      country "cyprus" "cy" "cyp"
      country "czech republic" "cz" "cze"
      country "cote d'ivoire" "ci" "civ"
      country "denmark" "dk" "dnk"
      country "djibouti" "dj" "dji"
      country "dominica" "dm" "dma"
      country "dominican republic" "do" "dom"
      country "ecuador" "ec" "ecu"
      country "egypt" "eg" "egy"
      country "el salvador" "sv" "slv"
      country "equatorial guinea" "gq" "gnq"
      country "eritrea" "er" "eri"
      country "estonia" "ee" "est"
      country "ethiopia" "et" "eth"
      country "falkland" "fk" "flk"
      country "maldivas" "fk" "flk"
      country "faroe islands" "fo" "fro"
      country "fiji" "fj" "fji"
      country "finland" "fi" "fin"
      country "france" "fr" "fra"
      country "french guiana" "gf" "guf"
      country "french polynesia" "pf" "pyf"
      country "french southern territories" "tf" "atf"
      country "gabon" "ga" "gab"
      country "gambia" "gm" "gmb"
      country "georgia" "ge" "geo"
      country "germany" "de" "deu"
      country "ghana" "gh" "gha"
      country "gibraltar" "gi" "gib"
      country "greece" "gr" "grc"
      country "greenland" "gl" "grl"
      country "grenada" "gd" "grd"
      country "guadeloupe" "gp" "glp"
      country "guam" "gu" "gum"
      country "guatemala" "gt" "gtm"
      country "guernsey" "gg" "ggy"
      country "guinea" "gn" "gin"
      country "guinea-bissau" "gw" "gnb"
      country "guyana" "gy" "guy"
      country "haiti" "ht" "hti"
      country "heard island and mcdonald islands" "hm" "hmd"
      country "vatican" "va" "vat"
      country "honduras" "hn" "hnd"
      country "hong kong" "hk" "hkg"
      country "hungary" "hu" "hun"
      country "iceland" "is" "isl"
      country "india" "in" "ind"
      country "indonesia" "id" "idn"
      country "iran" "ir" "irn"
      country "iraq" "iq" "irq"
      country "ireland" "ie" "irl"
      country "isle of man" "im" "imn"
      country "israel" "il" "isr"
      country "italy" "it" "ita"
      country "jamaica" "jm" "jam"
      country "japan" "jp" "jpn"
      country "jersey" "je" "jey"
      country "jordan" "jo" "jor"
      country "kazakhstan" "kz" "kaz"
      country "kenya" "ke" "ken"
      country "kiribati" "ki" "kir"
      country "north korea" "kp" "prk"
      country "south korea" "kr" "kor"
      country "kuwait" "kw" "kwt"
      country "kyrgyzstan" "kg" "kgz"
      country "lao" "la" "lao"
      country "latvia" "lv" "lva"
      country "lebanon" "lb" "lbn"
      country "lesotho" "ls" "lso"
      country "liberia" "lr" "lbr"
      country "libya" "ly" "lby"
      country "liechtenstein" "li" "lie"
      country "lithuania" "lt" "ltu"
      country "luxembourg" "lu" "lux"
      country "macao" "mo" "mac"
      country "macedonia" "mk" "mkd"
      country "madagascar" "mg" "mdg"
      country "malawi" "mw" "mwi"
      country "malaysia" "my" "mys"
      country "maldives" "mv" "mdv"
      country "mali" "ml" "mli"
      country "malta" "mt" "mlt"
      country "marshall islands" "mh" "mhl"
      country "martinique" "mq" "mtq"
      country "mauritania" "mr" "mrt"
      country "mauritius" "mu" "mus"
      country "mayotte" "yt" "myt"
      country "mexico" "mx" "mex"
      country "micronesia" "fm" "fsm"
      country "moldova" "md" "mda"
      country "monaco" "mc" "mco"
      country "mongolia" "mn" "mng"
      country "montenegro" "me" "mne"
      country "montserrat" "ms" "msr"
      country "morocco" "ma" "mar"
      country "mozambique" "mz" "moz"
      country "myanmar" "mm" "mmr"
      country "namibia" "na" "nam"
      country "nauru" "nr" "nru"
      country "nepal" "np" "npl"
      country "netherlands" "nl" "nld"
      country "new caledonia" "nc" "ncl"
      country "new zealand" "nz" "nzl"
      country "nicaragua" "ni" "nic"
      country "niger" "ne" "ner"
      country "nigeria" "ng" "nga"
      country "niue" "nu" "niu"
      country "norfolk island" "nf" "nfk"
      country "mariana islands" "mp" "mnp"
      country "norway" "no" "nor"
      country "oman" "om" "omn"
      country "pakistan" "pk" "pak"
      country "palau" "pw" "plw"
      country "palestine" "ps" "pse"
      country "panama" "pa" "pan"
      country "papua new guinea" "pg" "png"
      country "paraguay" "py" "pry"
      country "peru" "pe" "per"
      country "philippines" "ph" "phl"
      country "pitcairn" "pn" "pcn"
      country "poland" "pl" "pol"
      country "portugal" "pt" "prt"
      country "puerto rico" "pr" "pri"
      country "qatar" "qa" "qat"
      country "romania" "ro" "rou"
      country "russia" "ru" "rus"
      country "rwanda" "rw" "rwa"
      country "reunion" "re" "reu"
      country "saint barthelemy" "bl" "blm"
      country "saint helena" "sh" "shn"
      country "saint kitts and nevis" "kn" "kna"
      country "saint lucia" "lc" "lca"
      country "saint martin" "mf" "maf"
      country "saint pierre and miquelon" "pm" "spm"
      country "saint vincent and the grenadines" "vc" "vct"
      country "samoa" "ws" "wsm"
      country "san marino" "sm" "smr"
      country "sao tome and principe" "st" "stp"
      country "saudi arabia" "sa" "sau"
      country "senegal" "sn" "sen"
      country "serbia" "rs" "srb"
      country "seychelles" "sc" "syc"
      country "sierra leone" "sl" "sle"
      country "singapore" "sg" "sgp"
      country "sint maarten" "sx" "sxm"
      country "slovakia" "sk" "svk"
      country "slovenia" "si" "svn"
      country "solomon islands" "sb" "slb"
      country "somalia" "so" "som"
      country "south africa" "za" "zaf"
      country "south georgia and the south sandwich islands" "gs" "sgs"
      country "south sudan" "ss" "ssd"
      country "spain" "es" "esp"
      country "sri lanka" "lk" "lka"
      country "sudan" "sd" "sdn"
      country "suriname" "sr" "sur"
      country "svalbard and jan mayen" "sj" "sjm"
      country "swaziland" "sz" "swz"
      country "sweden" "se" "swe"
      country "switzerland" "ch" "che"
      country "syrian arab republic" "sy" "syr"
      country "taiwan" "tw" "twn"
      country "tajikistan" "tj" "tjk"
      country "united republic of tanzania" "tz" "tza"
      country "thailand" "th" "tha"
      country "timor-leste" "tl" "tls"
      country "togo" "tg" "tgo"
      country "tokelau" "tk" "tkl"
      country "tonga" "to" "ton"
      country "trinidad and tobago" "tt" "tto"
      country "tunisia" "tn" "tun"
      country "turkey" "tr" "tur"
      country "turkmenistan" "tm" "tkm"
      country "turks and caicos islands" "tc" "tca"
      country "tuvalu" "tv" "tuv"
      country "uganda" "ug" "uga"
      country "ukraine" "ua" "ukr"
      country "united arab emirates" "ae" "are"
      country "united kingdom" "gb" "gbr"
      country "united states" "us" "usa"
      country "united states minor outlying islands" "um" "umi"
      country "uruguay" "uy" "ury"
      country "uzbekistan" "uz" "uzb"
      country "vanuatu" "vu" "vut"
      country "venezuela" "ve" "ven"
      country "viet nam" "vn" "vnm"
      country "british virgin islands" "vg" "vgb"
      country "us virgin islands" "vi" "vir"
      country "wallis and futuna" "wf" "wlf"
      country "western sahara" "eh" "esh"
      country "yemen" "ye" "yem"
      country "zambia" "zm" "zmb"
      country "zimbabwe" "zw" "zwe" ]