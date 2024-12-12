using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SpanishVerbConjugator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rtbMessages.Text = "";
        }

        private void btnConjugate_Click(object sender, EventArgs e)
        {
             try {
                string strSelectedConjugation = this.cbConjugation.GetItemText(this.cbConjugation.SelectedItem);
                string strSpanishInfinitive = tbSpanishInfinitive.Text.Trim();
                string strSpanishInfinitiveCapitalized = "";
                string strEnglishInfinitive = tbEnglishInfinitive.Text.Trim(); 
                string strTodaysDate = "";
                string strThisYear = "";

                strTodaysDate = String.Format("{0:MMMM d, yyyy}", System.DateTime.Now);
                strThisYear = System.DateTime.Now.Year.ToString();

                // Get the capitalized infitive 
                System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                strSpanishInfinitiveCapitalized = textInfo.ToTitleCase(strSpanishInfinitive);

                // Get the stem of the infitive
                string strStem = strSpanishInfinitive.Remove(strSpanishInfinitive.Length - 2, 2);

                string strFirstPersonSingular = "";
                string strSecondPersonSingular = "";
                string strThirdPersonSingular = "";
                string strFirstPersonPlural = "";
                string strSecondPersonPlural = "";
                string strThirdPersonSPlural = "";

                rtbMessages.Text = "You selected " + strSelectedConjugation + "\r";
                rtbMessages.Text = rtbMessages.Text + "Spanish Verb: " + strSpanishInfinitive + "\r";
                rtbMessages.Text = rtbMessages.Text + "Spanish Verb Stem: " + strStem + "\r";
                rtbMessages.Text = rtbMessages.Text + "English Verb: to " + strEnglishInfinitive + "\r";

                // Create the HTML file
                string strFileName = "Espanol-Verbo-" + strSpanishInfinitiveCapitalized + ".html";
                FileInfo objFileInfo = new FileInfo(strFileName);
                StreamWriter objStreamWriter = objFileInfo.CreateText();

                objStreamWriter.WriteLine(@"<html xmlns:MSHelp=""http://msdn.microsoft.com/mshelp"">");
                objStreamWriter.WriteLine();
                objStreamWriter.WriteLine("<head>");
                objStreamWriter.WriteLine("<title>Español - " + strSpanishInfinitiveCapitalized + "</title>");
                objStreamWriter.WriteLine(@"<link rel=""stylesheet"" href=""css/Notes.css"" type=""text/css"">");
                objStreamWriter.WriteLine(@"<meta name=""keywords"" content=""Español,Verbo," + strSpanishInfinitiveCapitalized + @""">");
                objStreamWriter.WriteLine(@"<link rel=""stylesheet"" type=""text/css"" href=""ms-help://Hx/HxRuntime/HxLink.css"">");
                objStreamWriter.WriteLine(@"<link rel=""stylesheet"" type=""text/css"" href=""ms-help://Hx/HxRuntime/HxLinkDefault.css"">");
                objStreamWriter.WriteLine(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">");
                objStreamWriter.WriteLine("</head>");
                objStreamWriter.WriteLine();
                objStreamWriter.WriteLine(@"<body bgcolor=""#FFFFFF"">");
                objStreamWriter.WriteLine(@"<table border=""0"" width=""100%"">");
                objStreamWriter.WriteLine("  <tr>");
                objStreamWriter.WriteLine(@"    <td width=""100%""><span class=""PageTitle"">");
                objStreamWriter.WriteLine(@"	<img alt=""Flag of Spain"" class=""right"" height=""70"" src=""images/Spain-Flag.png"" width=""105"">Español - " + strSpanishInfinitiveCapitalized + "</span>");
                objStreamWriter.WriteLine("   <br>");
                objStreamWriter.WriteLine(@"    <span class=""Copyright"">Last modified: " + strTodaysDate + "<br>");
                objStreamWriter.WriteLine(@"    &copy; " + strThisYear + @" Robert Robbins. <span lang=""es"">Todos los derechos reservados.</span></span> </td>");
                objStreamWriter.WriteLine("  </tr>");
                objStreamWriter.WriteLine("</table>");
                objStreamWriter.WriteLine();

                objStreamWriter.WriteLine(@"<hr style=""border: 0; color: #000000; height: 1px;"" />");
                objStreamWriter.WriteLine(@"<span class=""SectionTitle""><span lang=""es"">" + strSpanishInfinitive + "</span> - to " + strEnglishInfinitive + "</span>");
                objStreamWriter.WriteLine(@"<p><strong lang=""es-es"">" + strSpanishInfinitive + ": </strong>to " + strEnglishInfinitive + "<br>");
                switch(strSelectedConjugation.Trim()) {
                    case "First-conjugation -ar verbs":
                    objStreamWriter.WriteLine(@"Regular first-conjugation Spanish verb<br>");
                    break;
                    case "Second-conjugation -er verbs":
                    objStreamWriter.WriteLine(@"Regular second-conjugation Spanish verb<br>");
                    break;
                    default:
                    objStreamWriter.WriteLine(@"Regular third-conjugation Spanish verb<br>");
                    break;
                }
                // Form the gerund
                string strGerund = "";
                string strPastParticiple = "";
                string strPresentParticiple = "";
                switch (strSelectedConjugation.Trim())
                {
                    case "First-conjugation -ar verbs":
                            strGerund = strStem + "ando";
                            strPresentParticiple = strStem + "ando";
                            strPastParticiple = strStem + "ado";
                         break;
                    default:
                            strGerund = strStem + "iendo";
                            strPresentParticiple = strStem + "iendo";
                            strPastParticiple = strStem + "ido";
                        break;
                }
                objStreamWriter.WriteLine(@"Transitive verb (takes a direct object) <br>");
                objStreamWriter.WriteLine(@"Intransitive verb (does not take a direct object) </p>");
                objStreamWriter.WriteLine(@"<p><b>infinitive:</b> <span lang=""es"">" + strSpanishInfinitive + "</span><br>");
                objStreamWriter.WriteLine(@"<b>gerund:</b> <span lang=""es"">" + strGerund + "</span><br>");
                objStreamWriter.WriteLine(@"<b>present participle:</b> <span lang=""es"">" + strPresentParticiple + "</span><br>");
                objStreamWriter.WriteLine(@"<b>past participle:</b> <span lang=""es"">" + strPastParticiple + "</span><br>");
                objStreamWriter.WriteLine(@"<b>auxiliary verb:</b> <span lang=""es"">haber</span></p>");
                objStreamWriter.WriteLine();

                 // Present Tense
                switch (strSelectedConjugation.Trim())
                {
                    case "First-conjugation -ar verbs":
                            strFirstPersonSingular = strStem + "o";
                            strSecondPersonSingular = strStem + "as";
                            strThirdPersonSingular = strStem + "a";
                            strFirstPersonPlural = strStem + "amos";
                            strSecondPersonPlural = strStem + "áis";
                            strThirdPersonSPlural = strStem + "an";
                        break;
                    case "Second-conjugation -er verbs":
                            strFirstPersonSingular = strStem + "o";
                            strSecondPersonSingular = strStem + "es";
                            strThirdPersonSingular = strStem + "e";
                            strFirstPersonPlural = strStem + "emos";
                            strSecondPersonPlural = strStem + "éis";
                            strThirdPersonSPlural = strStem + "en";
                        break;
                    default:
                            strFirstPersonSingular = strStem + "o";
                            strSecondPersonSingular = strStem + "es";
                            strThirdPersonSingular = strStem + "e";
                            strFirstPersonPlural = strStem + "imos";
                            strSecondPersonPlural = strStem + "ís";
                            strThirdPersonSPlural = strStem + "en";
                        break;
                }

                objStreamWriter.WriteLine(@"<hr style=""border: 0; color: #000000; height: 1px;"">");
                objStreamWriter.WriteLine(@"<span class=""SectionTitle""><span lang=""es"">El Presente</span> - The Present Tense</span>");
                objStreamWriter.WriteLine(@"<div class=""TableDiv"">");
                objStreamWriter.WriteLine(@"<table class=""tableData"">");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">yo " + strFirstPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>I " + strEnglishInfinitive + ", I am " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">nosotros " + strFirstPersonPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>we " + strEnglishInfinitive + ", we are " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">tú " + strSecondPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + ", you are " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">vosotros " + strSecondPersonPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + ", you are " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">usted " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + ", you are " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ustedes " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + ", you are " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">él " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>he " + strEnglishInfinitive + "s, he is " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellos " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>they " + strEnglishInfinitive + ", they are " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ella " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>she  " + strEnglishInfinitive + "s, she is " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellas " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>they " + strEnglishInfinitive + ", they are " + strEnglishInfinitive + "ing</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	</table>");
                objStreamWriter.WriteLine(@"</div>");

                 // Past Tense
                switch (strSelectedConjugation.Trim())
                {
                    case "First-conjugation -ar verbs":
                        strFirstPersonSingular = strStem + "é";
                        strSecondPersonSingular = strStem + "aste";
                        strThirdPersonSingular = strStem + "ó";
                        strFirstPersonPlural = strStem + "amos";
                        strSecondPersonPlural = strStem + "asteis";
                        strThirdPersonSPlural = strStem + "aron";
                        break;
                    case "Second-conjugation -er verbs":
                        strFirstPersonSingular = strStem + "í";
                        strSecondPersonSingular = strStem + "iste";
                        strThirdPersonSingular = strStem + "ió";
                        strFirstPersonPlural = strStem + "imos";
                        strSecondPersonPlural = strStem + "isteis";
                        strThirdPersonSPlural = strStem + "ieron";
                        break;
                    default:
                        strFirstPersonSingular = strStem + "í";
                        strSecondPersonSingular = strStem + "iste";
                        strThirdPersonSingular = strStem + "ió";
                        strFirstPersonPlural = strStem + "imos";
                        strSecondPersonPlural = strStem + "isteis";
                        strThirdPersonSPlural = strStem + "ieron";
                        break;
                }

                objStreamWriter.WriteLine(@"<hr style=""border: 0; color: #000000; height: 1px;"">");
                objStreamWriter.WriteLine(@"<span class=""SectionTitle""><span lang=""es"">El Pretérito</span> - The Past Tense</span>");
                objStreamWriter.WriteLine(@"<div class=""TableDiv"">");
                objStreamWriter.WriteLine(@"<table class=""tableData"">");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">yo " + strFirstPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>I " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">nosotros " + strFirstPersonPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>we " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">tú " + strSecondPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">vosotros " + strSecondPersonPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">usted " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ustedes " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>you " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">él " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>he " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellos " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>they " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ella " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>she " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellas " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>they " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	</table>");
                objStreamWriter.WriteLine(@"</div>");

                // Future Tense - uses the infinitive, not the stem
                // The same endings are used for all three types of verbs
                strFirstPersonSingular = strSpanishInfinitive + "é";
                strSecondPersonSingular = strSpanishInfinitive + "ás";
                strThirdPersonSingular = strSpanishInfinitive + "á";
                strFirstPersonPlural = strSpanishInfinitive + "emos";
                strSecondPersonPlural = strSpanishInfinitive + "éis";
                strThirdPersonSPlural = strSpanishInfinitive + "án";

                objStreamWriter.WriteLine(@"<hr style=""border: 0; color: #000000; height: 1px;"">");
                objStreamWriter.WriteLine(@"<span class=""SectionTitle""><span lang=""es"">El Futuro</span> - The Future Tense</span>");
                objStreamWriter.WriteLine(@"<div class=""TableDiv"">");
                objStreamWriter.WriteLine(@"<table class=""tableData"">");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">yo " + strFirstPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>I will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">nosotros " + strFirstPersonPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>we will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">tú " + strSecondPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>you will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">vosotros " + strSecondPersonPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>you will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">usted " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>you will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ustedes " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>you will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">él " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>he will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellos " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>they will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ella " + strThirdPersonSingular + "</td>");
                objStreamWriter.WriteLine(@"		<td>she will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellas " + strThirdPersonSPlural + "</td>");
                objStreamWriter.WriteLine(@"		<td>they will " + strEnglishInfinitive + "</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	</table>");
                objStreamWriter.WriteLine(@"</div>");

                // The Present Perfect Tense
                objStreamWriter.WriteLine(@"<hr style=""border: 0; color: #000000; height: 1px;"">");
                objStreamWriter.WriteLine(@"<span class=""SectionTitle""><span lang=""es"">El Presente Perfecto</span> - The Present Perfect Tense</span>");
                objStreamWriter.WriteLine(@"<div class=""TableDiv"">");
                objStreamWriter.WriteLine(@"<table class=""tableData"">");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"		<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">yo he " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>I have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">nosotros hemos " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>we have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">tú has " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>you have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">vosotros habéis " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>you have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">usted ha " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>you have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ustedes han " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>you have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">él ha " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>he has " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellos han " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>they have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	<tr>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ella ha " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>she has " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"		<td lang=""es-ES"">ellas han " + strPastParticiple + "</td>");
                objStreamWriter.WriteLine(@"		<td>they have " + strEnglishInfinitive + "ed</td>");
                objStreamWriter.WriteLine(@"	</tr>");
                objStreamWriter.WriteLine(@"	</table>");
                objStreamWriter.WriteLine(@"</div>");

                 objStreamWriter.WriteLine(@"<p>The perfect tense in Spanish has two parts to it. The present tense of");
                objStreamWriter.WriteLine(@"the verb <strong>haber</strong> (to have) plus the past participle. The present prefect tense"); 
                objStreamWriter.WriteLine(@"expresses past action closely related to the present. The English equivalent");
                objStreamWriter.WriteLine(@"of <strong>haber</strong> + past participle is <em>to have done something</em>.</p>");
                objStreamWriter.WriteLine(@"<p><img alt=""Note"" height=""10"" src=""images/note.gif"" width=""10""><span class=""NoteText"">"); 
                objStreamWriter.WriteLine(@"This is the present perfect indicative, not the present perfect subjunctive.</span></p>");

                // Examples
                objStreamWriter.WriteLine(@"<hr style=""border: 0; color: #000000; height: 1px;"">");
                objStreamWriter.WriteLine(@"<span class=""SectionTitle""><span lang=""es"">Ejemplos</span> - Examples</span>");
                objStreamWriter.WriteLine(@"<div class=""TableDiv"">");
                objStreamWriter.WriteLine(@"<table class=""tableData"">");
                objStreamWriter.WriteLine(@"	<thead>");
                objStreamWriter.WriteLine(@"		<tr>");
                objStreamWriter.WriteLine(@"			<th lang=""es-ES"">Español</th>");
                objStreamWriter.WriteLine(@"			<th lang=""es-ES"">Inglés</th>");
                objStreamWriter.WriteLine(@"		</tr>");
                objStreamWriter.WriteLine(@"	</thead>");
                objStreamWriter.WriteLine(@"	<tbody>");
                objStreamWriter.WriteLine(@"		<tr>");
                objStreamWriter.WriteLine(@"			<td lang=""es-ES"">&nbsp;</td>");
                objStreamWriter.WriteLine(@"			<td>&nbsp;</td>");
                objStreamWriter.WriteLine(@"		</tr>");
                objStreamWriter.WriteLine(@"		<tr>");
                objStreamWriter.WriteLine(@"			<td lang=""es-ES"">&nbsp;</td>");
                objStreamWriter.WriteLine(@"			<td>&nbsp;</td>");
                objStreamWriter.WriteLine(@"		</tr>");
                objStreamWriter.WriteLine(@"		<tr>");
                objStreamWriter.WriteLine(@"			<td lang=""es-ES"">&nbsp;</td>");
                objStreamWriter.WriteLine(@"			<td>&nbsp;</td>");
                objStreamWriter.WriteLine(@"		</tr>");
                objStreamWriter.WriteLine(@"	</tbody>");
                objStreamWriter.WriteLine(@"</table>");
                objStreamWriter.WriteLine(@"</div>");


                objStreamWriter.WriteLine(@"<hr style=""border: 0; color: #000000; height: 1px;"" />");
                objStreamWriter.WriteLine(@"<font color=""#000000"" face=""Verdana,Arial"" size=""1"">&copy; " + strThisYear + @" Robert Robbins. <span lang=""es"">Todos los derechos reservados.</span></font>");
                objStreamWriter.WriteLine("</body>");
                objStreamWriter.WriteLine("</html>");

                objStreamWriter.Close();
                rtbMessages.Text = rtbMessages.Text + "Created HTML File.";
             }
             catch (Exception ex)
             {
                 rtbMessages.ForeColor = Color.Red;
                 rtbMessages.Text = rtbMessages.Text + ex.ToString();
             }

        }




    }
}
