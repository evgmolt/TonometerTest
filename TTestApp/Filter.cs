﻿namespace TTestApp
{
    public static class Filter
    {
        public static readonly double[] coeff10Hz =  {
  1.935195791367e-05,-8.110754917992e-05,-0.0001822260854116,-0.0002836140664768,
   -0.00038487703573,-0.0004856171197516,-0.0005854345102264,-0.0006839289668642,
  -0.000780701335971,-0.0008753550791812,-0.0009674978067756,-0.001056742809951,
  -0.001142710586363, -0.00122503035324,-0.001303341542373,-0.001377295271291,
  -0.001446555784991,-0.001510801862628, -0.00156972818368,-0.001623046648178,
  -0.001670487645735,-0.001711801268235,-0.001746758461217,-0.001775152109153,
  -0.001796798050046,-0.001811536014953,-0.001819230488326,-0.001819771485244,
  -0.001813075241963,-0.001799084816406,-0.001777770595584,-0.001749130707202,
  -0.001713191333046, -0.00167000692208, -0.00161966030151,-0.001562262684453,
  -0.001497953573166,-0.001426900557196,-0.001349299006163,-0.001265371657254,
   -0.00117536809791,-0.001079564144538,-0.0009782611184676,-0.0008717850207654,
  -0.0007604856078594,-0.0006447353703284,-0.0005249284175541,-0.0004014792712994,
  -0.0002748215716236,-0.0001454066988832,-1.370231589837e-05,0.0001198091653213,
  0.0002546321875909,0.0003902597183173,0.0005261749797552,0.0006618532342331,
  0.0007967636185885,0.0009303710218291, 0.001062137999837, 0.001191526720748,
   0.001318000934477, 0.001441027959719, 0.001560080681649, 0.001674639553425,
   0.001784194594551, 0.001888247379101,  0.00198631300677, 0.002077922049738,
   0.002162622468336, 0.002239981488569,  0.00230958743462, 0.002371051509544,
   0.002424009517509, 0.002468123521048, 0.002503083427003, 0.002528608494986,
   0.002544448762429, 0.002550386380524, 0.002546236855597, 0.002531850190762,
   0.002507111922978, 0.002471944050955, 0.002426305849697, 0.002370194567814,
   0.002303646004091, 0.002226734960206, 0.002139575566863, 0.002042321481024,
   0.001935165952325, 0.001818341757215, 0.001692120999759, 0.001556814778502,
   0.001412772719241, 0.001260382373986, 0.001100068486856,0.0009322921280937,
  0.0007575496978421,0.0005763718017675,0.0003893220010617,0.0001969954397882,
  1.735296979612e-08,-0.0002009585407637,-0.0004052517577375,-0.0006121568743751,
  -0.0008209454539057,-0.001030868069248,-0.001241156416328, -0.00145102551174,
  -0.001659675968353,-0.001866296342153,-0.002070065543325,-0.002270155304343,
  -0.002465732697557,-0.002655962694609,-0.002840010759757,-0.003017045469078,
  -0.003186241147343,-0.003346780514267,-0.003497857331739,-0.003638679043582,
  -0.003768469399372,-0.003886471053815,-0.003991948133247, -0.00408418876082,
  -0.004162507532087,-0.004226247932724,-0.004274784690349,-0.004307526052485,
  -0.004323915982961,-0.004323436269219,-0.004305608533268,-0.004269996139268,
   -0.00421620599104,-0.004143890213077,-0.004052747709021,-0.003942525591867,
  -0.003813020480577,-0.003664079658171, -0.00349560208676,-0.003307539275447,
  -0.003099895997436,-0.002872730853176,-0.002626156676822,-0.002360340783775,
  -0.002075505057583,-0.001771925874935,-0.001449933868039,-0.001109913524148,
  -0.0007523026225219,-0.0003775915096282, 1.36777860985e-05,0.0004209125981226,
  0.0008434708219611, 0.001280662268796, 0.001731750169216, 0.002195952823285,
   0.002672445392643, 0.003160361829906,  0.00365879694016,  0.00416680856893,
   0.004683419910582, 0.005207621930716, 0.005738375895723, 0.006274616002349,
   0.006815252099733, 0.007359172496103, 0.007905246842019, 0.008452329081766,
   0.008999260464313, 0.009544872604977,   0.0100879905888,  0.01062743610648,
    0.01116203061354,  0.01169059850338,  0.01221197028471,  0.01272498575394,
    0.01322849715293,  0.01372137230275,  0.01420249770388,  0.01467078159363,
    0.01512515695147,  0.01556458444325,  0.01598805529534,  0.01639459408998,
     0.0167832614734,  0.01715315676843,  0.01750342048363,  0.01783323671137,
    0.01814183540752,  0.01842849454568,  0.01869254213955,  0.01893335812715,
    0.01915037611113,  0.01934308494987,  0.01951103019443,   0.0196538153671,
    0.01977110307747,   0.0198626159726,  0.01992813751857,  0.01996751261072,
    0.01998064801097,  0.01996751261072,  0.01992813751857,   0.0198626159726,
    0.01977110307747,   0.0196538153671,  0.01951103019443,  0.01934308494987,
    0.01915037611113,  0.01893335812715,  0.01869254213955,  0.01842849454568,
    0.01814183540752,  0.01783323671137,  0.01750342048363,  0.01715315676843,
     0.0167832614734,  0.01639459408998,  0.01598805529534,  0.01556458444325,
    0.01512515695147,  0.01467078159363,  0.01420249770388,  0.01372137230275,
    0.01322849715293,  0.01272498575394,  0.01221197028471,  0.01169059850338,
    0.01116203061354,  0.01062743610648,   0.0100879905888, 0.009544872604977,
   0.008999260464313, 0.008452329081766, 0.007905246842019, 0.007359172496103,
   0.006815252099733, 0.006274616002349, 0.005738375895723, 0.005207621930716,
   0.004683419910582,  0.00416680856893,  0.00365879694016, 0.003160361829906,
   0.002672445392643, 0.002195952823285, 0.001731750169216, 0.001280662268796,
  0.0008434708219611,0.0004209125981226, 1.36777860985e-05,-0.0003775915096282,
  -0.0007523026225219,-0.001109913524148,-0.001449933868039,-0.001771925874935,
  -0.002075505057583,-0.002360340783775,-0.002626156676822,-0.002872730853176,
  -0.003099895997436,-0.003307539275447, -0.00349560208676,-0.003664079658171,
  -0.003813020480577,-0.003942525591867,-0.004052747709021,-0.004143890213077,
   -0.00421620599104,-0.004269996139268,-0.004305608533268,-0.004323436269219,
  -0.004323915982961,-0.004307526052485,-0.004274784690349,-0.004226247932724,
  -0.004162507532087, -0.00408418876082,-0.003991948133247,-0.003886471053815,
  -0.003768469399372,-0.003638679043582,-0.003497857331739,-0.003346780514267,
  -0.003186241147343,-0.003017045469078,-0.002840010759757,-0.002655962694609,
  -0.002465732697557,-0.002270155304343,-0.002070065543325,-0.001866296342153,
  -0.001659675968353, -0.00145102551174,-0.001241156416328,-0.001030868069248,
  -0.0008209454539057,-0.0006121568743751,-0.0004052517577375,-0.0002009585407637,
  1.735296979612e-08,0.0001969954397882,0.0003893220010617,0.0005763718017675,
  0.0007575496978421,0.0009322921280937, 0.001100068486856, 0.001260382373986,
   0.001412772719241, 0.001556814778502, 0.001692120999759, 0.001818341757215,
   0.001935165952325, 0.002042321481024, 0.002139575566863, 0.002226734960206,
   0.002303646004091, 0.002370194567814, 0.002426305849697, 0.002471944050955,
   0.002507111922978, 0.002531850190762, 0.002546236855597, 0.002550386380524,
   0.002544448762429, 0.002528608494986, 0.002503083427003, 0.002468123521048,
   0.002424009517509, 0.002371051509544,  0.00230958743462, 0.002239981488569,
   0.002162622468336, 0.002077922049738,  0.00198631300677, 0.001888247379101,
   0.001784194594551, 0.001674639553425, 0.001560080681649, 0.001441027959719,
   0.001318000934477, 0.001191526720748, 0.001062137999837,0.0009303710218291,
  0.0007967636185885,0.0006618532342331,0.0005261749797552,0.0003902597183173,
  0.0002546321875909,0.0001198091653213,-1.370231589837e-05,-0.0001454066988832,
  -0.0002748215716236,-0.0004014792712994,-0.0005249284175541,-0.0006447353703284,
  -0.0007604856078594,-0.0008717850207654,-0.0009782611184676,-0.001079564144538,
   -0.00117536809791,-0.001265371657254,-0.001349299006163,-0.001426900557196,
  -0.001497953573166,-0.001562262684453, -0.00161966030151, -0.00167000692208,
  -0.001713191333046,-0.001749130707202,-0.001777770595584,-0.001799084816406,
  -0.001813075241963,-0.001819771485244,-0.001819230488326,-0.001811536014953,
  -0.001796798050046,-0.001775152109153,-0.001746758461217,-0.001711801268235,
  -0.001670487645735,-0.001623046648178, -0.00156972818368,-0.001510801862628,
  -0.001446555784991,-0.001377295271291,-0.001303341542373, -0.00122503035324,
  -0.001142710586363,-0.001056742809951,-0.0009674978067756,-0.0008753550791812,
  -0.000780701335971,-0.0006839289668642,-0.0005854345102264,-0.0004856171197516,
   -0.00038487703573,-0.0002836140664768,-0.0001822260854116,-8.110754917992e-05,
  1.935195791367e-05
        };
        public static readonly double[] coeff50 =  {
                                    +5.8055825294890924e-003, 
                                   -5.6110169116486645e-003, 
                                   -1.3295678981616035e-002, 
                                   +1.9963872393967758e-002, 
                                   -5.5108638151477171e-003, 
                                   -3.6636514465905511e-002, 
                                   +5.7692568525026300e-002, 
                                   -6.1187785202242596e-003, 
                                   -1.2758195775567843e-001, 
                                   +2.7500984976154336e-001, 
                                   +6.6022919178123562e-001, 
                                   +2.7500984976154336e-001, 
                                   -1.2758195775567843e-001, 
                                   -6.1187785202242596e-003, 
                                   +5.7692568525026300e-002, 
                                   -3.6636514465905511e-002, 
                                   -5.5108638151477171e-003, 
                                   +1.9963872393967758e-002, 
                                   -1.3295678981616035e-002, 
                                   -5.6110169116486645e-003, 
                                   +5.8055825294890924e-003 };

        //LPF_Fs125Hz_Fpass8Hz_Fstop14Hz
        public static readonly double[] coeff14 = {
                                    +1.4955781797938609e-002, 
                                    +2.4870721049448024e-002, 
                                    +4.1110724254332091e-002, 
                                    +5.9668660137318891e-002, 
                                    +7.8249451320493643e-002, 
                                    +9.4172718549901807e-002, 
                                    +1.0493367973445666e-001, 
                                    +1.0874215210149615e-001, 
                                    +1.0493367973445666e-001, 
                                    +9.4172718549901807e-002, 
                                    +7.8249451320493643e-002, 
                                    +5.9668660137318891e-002, 
                                    +4.1110724254332091e-002, 
                                    +2.4870721049448024e-002, 
                                    +1.4955781797938609e-002 };


        public static int FilterForRun(double[] coeff, int[] inArr, uint ind)
        {
            double sum = 0;
            for (int i = 0; i < coeff.Length; i++)
            {
                double a;
                a = inArr[(ind - coeff.Length + i + 1) & (ByteDecomposer.DataArrSize - 1)];
                a *= coeff[i];
                sum += a;
            }
            return (int)Math.Round(sum);
        }

        public static double FilterForView(double[] coeff, double[] inArr, int ind)
        {
            if (ind < coeff.Length) return 0;
            double sum = 0;
            for (int i = 0; i < coeff.Length; i++)
            {
                double a;
                a = inArr[ind - coeff.Length + i + 1];
                a *= coeff[i];
                sum += a;
            }
            return sum;
        }

        public static double Median(int windowSize, double[] inArr, int ind)
        {
            if (ind > inArr.Length - windowSize)
            {
                return 0;
            }
            double[] tmpArr = new double[windowSize];
            for (int i = 0; i < windowSize; i++)
            {
                tmpArr[i] = inArr[ind + i];
            }
            Array.Sort(tmpArr);
            if (tmpArr.Length %2 == 0)
            {
                return (tmpArr[tmpArr.Length / 2] + tmpArr[tmpArr.Length / 2 - 1] ) / 2; 
            }
            else 
            {
                return tmpArr[tmpArr.Length / 2];
            }
        }
    }
}
