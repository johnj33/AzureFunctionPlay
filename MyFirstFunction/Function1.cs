using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace MyFirstFunction
{
    public static class Function1
    {
        private const string MySvg =
            "<style>    \r\n.johnsDrawing {\r\n  height: 550px;\r\n  width: 100%;\r\n}\r\n.johnsDrawing .path {\r\n  stroke-dasharray: 1000;\r\n  stroke-dashoffset: 1000;\r\n  animation: dash 5s linear alternate infinite;\r\n}\r\n@keyframes dash {\r\n  from {\r\n    stroke-dashoffset: 1000;\r\n  }\r\n  to {\r\n    stroke-dashoffset: 0;\r\n  }\r\n}\r\n</style>\r\n<svg class=\"johnsDrawing\">\r\n          <path class=\"path\" width=\"108.81322957198441\" height=\"24.309338521400775\" stroke-miterlimit=\"3\" stroke=\"#000000\" fill=\"#FFFFFF\"\r\n            d=\"M71.40856031128405,120.38910505836574 C105.41739407981893,113.22935058077942 146.1198414481934,107.44708268199003 180.22178988326849,96.07976653696497 \"\r\n          />\r\n          <path class=\"path\" width=\"198.26321824752807\" height=\"111.31224999199091\" stroke-miterlimit=\"3\" stroke=\"#000000\" fill=\"#FFFFFF\"\r\n            d=\"M130.44552529182877,108.81322957198441 C150.36510896446273,137.787169459452 190.62841258619656,162.1439134530442 184.85214007782102,202.5778210116731 C182.92198845515256,216.08888237035222 137.16121068480336,234.7688205290538 131.60311284046693,201.420233463035 C130.70179915641606,196.01235135873014 126.92392528442335,193.69198289233688 130.44552529182877,187.5291828793774 C131.9407305736648,184.91257363616435 134.66010222931337,183.01568770154736 137.39105058365757,181.74124513618673 C162.42876364116185,170.05697904268476 193.73338257610905,173.63813229571983 220.73735408560307,173.63813229571983 C224.2101167315175,173.63813229571983 229.60257535414897,170.53199895732516 231.1556420233463,173.63813229571983 C236.87059231017528,185.06803286937782 231.9373124679676,201.68079027790873 248.51945525291828,207.20817120622564 C268.8666368172338,213.99056506099754 323.6090170014354,220.16670690925997 327.23540856031127,187.5291828793774 C329.60132827257644,166.23590546899086 264.5780550840965,163.61996063245286 254.30739299610892,171.32295719844356 C247.35675271173946,176.5359374117206 245.27263436012808,183.60453672721457 241.57392996108948,191.0019455252918 \"\r\n          />\r\n          <path class=\"path\" width=\"248.71174154098105\" height=\"180.48611721989298\" stroke-miterlimit=\"3\" stroke=\"#000000\" fill=\"#FFFFFF\"\r\n            d=\"M333.02334630350185,67.14007782101166 C352.48356515929527,115.1419509986354 377.9108840559835,163.09682877286144 387.4299610894941,214.15369649805444 C388.99225713663577,222.53328438726902 388.7680824006336,231.15282308824135 389.7451361867703,239.62062256809332 C389.97066146899465,241.57517501403737 392.87024423514106,245.40856031128402 390.9027237354085,245.40856031128402 C389.7220564446153,245.40856031128402 387.10814529441575,259.539722837123 389.7451361867703,216.4688715953307 C390.45117527601644,204.93689980431145 388.10352164600135,156.82152391307082 407.1089494163424,150.4863813229572 C441.2722765976267,139.0986055958624 454.7074527552512,200.25329178257732 458.04280155642016,221.09922178988325 C458.8005163135557,225.83493902198006 457.2205661079199,233.17005302554912 463.83073929961085,231.51750972762642 C471.736830624596,229.54098689638016 484.35074286139786,208.63925380216432 488.1400778210116,203.73540856031127 C490.83014696242924,200.25414261259436 493.6860276573184,196.89714882839212 496.2431906614785,193.31712062256807 C508.85281793488866,175.6636424397938 501.59957620194416,207.5026543494139 507.81906614785987,219.94163424124508 C509.2103132353444,222.72412841621406 506.95642239188135,213.77784367506706 506.6614785992217,210.68093385214007 C506.0715910139023,204.48711420628604 504.90960298238315,198.35582735874402 504.3463035019455,192.15953307392994 C503.33848922358413,181.0735760119558 498.03561014493573,149.11718312937933 514.7645914396886,143.54085603112838 C546.3184508938092,133.02290287975484 568.0341813149007,191.21604287164246 573.8015564202334,210.68093385214007 C575.4808599529695,216.34858327512433 587.4703385967206,234.99027237354085 578.4319066147859,234.99027237354085 \"\r\n          />\r\n          <path class=\"path\" width=\"192.0819525311915\" height=\"76.25619889149243\" stroke-miterlimit=\"3\" stroke=\"#000000\" fill=\"#FFFFFF\"\r\n            d=\"M122.34241245136187,347.27626459143966 C140.67900135701646,393.11773685557625 174.99851955417782,370.4582725148623 209.1614785992218,347.27626459143966 C213.45711157338073,344.36137078754604 215.54610485861937,336.8579766536965 220.73735408560307,336.8579766536965 C222.4948235903648,336.8579766536965 226.39611102510085,350.6844368355076 228.84046692607004,351.9066147859922 C240.77936081306032,357.87606172948733 284.5204950669329,347.95298745022234 286.7198443579766,347.27626459143966 C312.07719586286476,339.4740025899356 324.2689162927313,297.49999999999994 305.24124513618676,297.49999999999994 \"\r\n          />\r\n          <path class=\"path\" width=\"57.453811117604005\" height=\"35.64218724390656\" stroke-miterlimit=\"3\" stroke=\"#000000\" fill=\"#FFFFFF\"\r\n            d=\"M375.8540856031128,310.23346303501944 C369.1004994243784,317.7815887641931 351.64552492786333,324.32606931097956 356.1750972762645,333.38521400778205 C357.49151165012915,336.0180427555113 372.2787087101781,339.50440042457916 373.5389105058365,336.8579766536965 C384.0879580045571,314.70497690638337 380.48443579766536,302.1303501945525 380.48443579766536,302.1303501945525 C380.48443579766536,302.1303501945525 382.60723098154347,306.1183313216949 383.9571984435797,307.9182879377432 C392.3423777821338,319.0985270558152 397.5148633259588,329.9124513618677 412.896887159533,329.9124513618677 \"\r\n          />\r\n          <path class=\"path\" width=\"31.254863813229576\" height=\"57.87937743190662\" stroke-miterlimit=\"3\" stroke=\"#000000\" fill=\"#FFFFFF\"\r\n            d=\"M458.04280155642016,284.7665369649805 C447.11469620543267,287.2501972720231 434.8448687826019,287.12789386331247 426.7879377431906,295.1848249027237 C424.26953853345753,297.7032241124568 449.7917358205045,312.4006852368471 452.25486381322946,314.8638132295719 C463.83153517644894,326.4404845927914 451.7749135222215,342.6459143968871 438.36381322957186,342.6459143968871 \"\r\n          />\r\n          <path class=\"path\" width=\"320.65175097276256\" height=\"96.33157751481605\" stroke-miterlimit=\"3\" stroke=\"#000000\" fill=\"#FFFFFF\"\r\n            d=\"M214.94941634241243,413.2587548638132 C218.2248993342342,445.19471403407545 218.4221789883268,470.431917947529 218.4221789883268,501.23540856031127 C218.4221789883268,503.160859223104 218.68817235139224,503.81657702065286 219.57976653696497,504.70817120622564 C219.74258001602712,504.8709846852878 224.1182921419671,504.8459080905511 224.2101167315175,504.70817120622564 C233.16735508077122,491.27231368234504 244.55346682294996,438.12977356147996 264.7256809338521,434.0953307392996 C285.2427654698454,429.9919138321009 265.7683942178809,473.39587026069137 275.14396887159535,478.0836575875486 C291.94663872957176,486.48499251653675 334.4235759477799,471.40362591075393 349.2295719844358,463.03501945525284 C353.544724778916,460.596020049677 365.0097276231162,443.33190661278024 357.3326848249027,438.72568093385206 C329.39574689333676,421.9635181749126 313.5864855912838,464.8621986336964 327.23540856031127,469.9805447470817 C354.9371798023142,480.3687089628328 403.6361867704279,468.82654870704437 403.6361867704279,432.9377431906614 C403.6361867704279,431.3942931258106 403.6361867704279,436.0246433203631 403.6361867704279,437.56809338521396 C403.6361867704279,442.1984435797665 403.6361867704279,446.828793774319 403.6361867704279,451.45914396887156 C403.6361867704279,452.61673151750966 403.6361867704279,456.0894941634241 403.6361867704279,454.93190661478593 C403.6361867704279,440.5768766846077 405.9073960586004,419.74585618545746 417.5272373540855,409.7859922178988 C427.343814254633,401.371783446001 450.9292488857873,437.7835282414215 461.51556420233453,441.0408560311283 C478.11601684841014,446.14868761453624 504.1706736940315,436.14970998194576 513.6070038910505,422.51945525291825 C516.0626179074279,418.972457229262 520.1074456031808,410.8323088716123 515.9221789883268,409.7859922178988 C488.81682101810406,403.0096527253431 445.1943071931629,421.2468363760423 474.24902723735397,450.3015564202334 C489.31268739348775,465.3652165763672 517.2894432765279,458.0255049370703 535.601167315175,465.35019455252916 \"\r\n          />\r\n        </svg>";
        
        [FunctionName("JohnsFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(MySvg)};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}