using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedQuizData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "Title", "Subtitle" },
                values: new object[,]
                {
                    { 1, "Ce tip de experiență preferați atunci când conduceți?", "Alegeți varianta care vi se potrivește cel mai bine." },
                    { 2, "În ce scop utilizați cel mai des mașina?", "Selectați utilizarea principală." },
                    { 3, "Ce tip de caroserie preferați?", "Alegeți forma dorită." },
                    { 4, "Cât de important este consumul redus de combustibil pentru dumneavoastră?", "Selectați nivelul de importanță." },
                    { 5, "Ce tip de motorizare preferați?", "Alegeți sursa de putere." },
                    { 6, "Ce contează cel mai mult la o mașină?", "Alegeți caracteristica principală." },
                    { 7, "Cât de des conduceți pe distanțe lungi?", "Estimați frecvența deplasărilor lungi." },
                    { 8, "Ce buget aproximativ ați aloca pentru achiziționarea unei mașini?", "Alegeți intervalul de preț." },
                    { 9, "Cât de importantă este tehnologia modernă în alegerea unei mașini?", "Selectați nivelul de importanță." },
                    { 10, "Ce caracteristică vă reprezintă cel mai bine stilul de condus?", "Alegeți varianta potrivită." },
                    { 11, "Ce marcă auto preferați cel mai mult?", "Selectați brandul preferat." },
                    { 12, "Ce tip de transmisie preferați?", "Alegeți cutia de viteze." },
                    { 13, "Cât de importantă este siguranța în alegerea unei mașini?", "Selectați nivelul de importanță." },
                    { 14, "Ce tip de combustibil considerați că va fi cel mai potrivit pentru viitor?", "Alegeți varianta potrivită." }
                });

            // 2. Inserăm cele 57 de Opțiuni FĂRĂ coloana Weight
            migrationBuilder.InsertData(
            table: "QuizOptions",
            columns: new[] { "Id", "Label", "Description", "QuizQuestionId" },
            values: new object[,]
            {
                // Întrebarea 1: Experiență
                { 1, "Confort și relaxare", "Prioritizează un drum lin și o călătorie odihnitoare.", 1 },
                { 2, "Sportivitate și performanță", "Apreciază accelerația rapidă și manevrabilitatea precisă.", 1 },
                { 3, "Un echilibru între cele două", "Caută un compromis ideal între dinamică și confort zilnic.", 1 },

                // Întrebarea 2: Scop
                { 4, "Deplasări zilnice în oraș", "Naveta zilnică, aglomerație urbană și distanțe scurte.", 2 },
                { 5, "Drumuri lungi și vacanțe", "Călătorii prelungite pe autostradă și explorarea de locuri noi.", 2 },
                { 6, "Activități de serviciu", "Mașina este utilizată predominant în interes profesional.", 2 },
                { 7, "Condus sportiv / hobby", "Ieșiri de weekend pe drumuri virajate sau pasiune auto.", 2 },

                // Întrebarea 3: Caroserie
                { 8, "Sedan", "Formă clasică, elegantă, cu portbagaj separat.", 3 },
                { 9, "SUV", "Poziție înaltă la volan, gardă la sol mărită și siguranță sporită.", 3 },
                { 10, "Hatchback", "Compactă, practică în oraș, cu hayon pentru portbagaj.", 3 },
                { 11, "Coupe", "Design sportiv, plafon plonjat, de obicei cu 2 uși.", 3 },
                { 12, "Break", "Varianta alungită a unui sedan, oferind spațiu maxim de încărcare.", 3 },

                // Întrebarea 4: Consum
                { 13, "Foarte important", "Costurile de rulare trebuie menținute la un nivel minim absolut.", 4 },
                { 14, "Important", "Preferă o mașină economică, dar acceptă mici compromisuri.", 4 },
                { 15, "Moderat", "Consumul contează, dar performanța sau confortul primează.", 4 },
                { 16, "Deloc important", "Costul combustibilului nu influențează decizia de cumpărare.", 4 },

                // Întrebarea 5: Motorizare
                { 17, "Benzină", "Funcționare rafinată, ideală pentru oraș și parcurs mixt.", 5 },
                { 18, "Diesel", "Cuplu ridicat și consum excelent la drumuri foarte lungi.", 5 },
                { 19, "Hibrid", "Eficiență maximă în oraș și autonomie relaxată la drum întins.", 5 },
                { 20, "Electrică", "Zero emisii, silențioasă și cu încărcare la stație sau acasă.", 5 },
                { 21, "Nu am o preferință", "Deschis la orice tehnologie care se potrivește bugetului.", 5 },

                // Întrebarea 6: Ce contează cel mai mult
                { 22, "Designul", "Aspectul exterior și interior trebuie să atragă privirile.", 6 },
                { 23, "Performanța", "Puterea motorului și dinamica de condus sunt prioritare.", 6 },
                { 24, "Confortul", "Scaunele, suspensia moale și antifonarea trebuie să fie de top.", 6 },
                { 25, "Tehnologia și dotările", "Sisteme de infotainment avansate și asistență la condus.", 6 },
                { 26, "Siguranța", "Structură solidă și sisteme active de prevenire a accidentelor.", 6 },

                // Întrebarea 7: Distanțe lungi
                { 27, "Foarte des", "Călătorii interurbane regulate, săptămânal sau lunar.", 7 },
                { 28, "Ocazional", "Câteva ieșiri din oraș pe an sau vacanțe planificate.", 7 },
                { 29, "Rar", "Majoritatea kilometrilor sunt parcurși în proximitatea casei.", 7 },
                { 30, "Aproape niciodată", "Mașina nu părăsește aproape deloc mediul urban.", 7 },

                // Întrebarea 8: Buget
                { 31, "Sub 10.000 €", "Buget orientat spre mașini rulate accesibile și fiabile.", 8 },
                { 32, "10.000 – 20.000 €", "Acoperă mașini rulate bune sau vehicule noi din clasa mică.", 8 },
                { 33, "20.000 – 35.000 €", "Buget generos pentru mașini noi, bine dotate, sau premium rulate.", 8 },
                { 34, "Peste 35.000 €", "Segmentul premium, performanță înaltă sau vehicule electrice de top.", 8 },

                // Întrebarea 9: Tehnologie
                { 35, "Foarte importantă", "Ecrane mari, conectivitate permanentă și funcții inteligente.", 9 },
                { 36, "Importantă", "Dotări moderne esențiale, precum conectarea la smartphone.", 9 },
                { 37, "Puțin importantă", "Preferă butoanele fizice și simplitatea, fără sisteme complexe.", 9 },
                { 38, "Deloc importantă", "Atenția este strict pe condus, tehnologia este un detaliu ignorat.", 9 },

                // Întrebarea 10: Stil condus
                { 39, "Calm și prudent", "Conduce defensiv, respectând cu strictețe limitele de viteză.", 10 },
                { 40, "Echilibrat", "Se adaptează traficului, menținând un ritm fluid și sigur.", 10 },
                { 41, "Dinamic și rapid", "Preferă un ritm alert și accelerații ferme acolo unde este permis.", 10 },
                { 42, "Adaptabil în funcție de situație", "Își schimbă stilul de condus în funcție de condiții și trafic.", 10 },

                // Întrebarea 11: Marcă
                { 43, "BMW", "Focus pe dinamica de condus, sportivitate și tracțiune spate/integrală.", 11 },
                { 44, "Audi", "Tehnologie avansată, interioare rafinate și tracțiune quattro.", 11 },
                { 45, "Mercedes-Benz", "Eleganță absolută, lux interior și confort suprem la rulare.", 11 },

                // Întrebarea 12: Transmisie
                { 46, "Manuală", "Implicare activă în condus și control mecanic asupra turației.", 12 },
                { 47, "Automată", "Confort maxim, elimină stresul schimbării treptelor în aglomerație.", 12 },
                { 48, "Nu contează", "Acceptă orice cutie de viteze atâta timp cât mașina îi place.", 12 },

                // Întrebarea 13: Siguranță
                { 49, "Foarte importantă", "Fără niciun compromis când vine vorba de protecția pasagerilor.", 13 },
                { 50, "Importantă", "Dorește rezultate bune la testele de impact și asistență activă.", 13 },
                { 51, "Moderată", "Pachetul standard de airbag-uri și sisteme de bază sunt suficiente.", 13 },
                { 52, "Puțin importantă", "Alte caracteristici ale mașinii primează în decizia de achiziție.", 13 },

                // Întrebarea 14: Viitor
                { 53, "Benzină", "Motoarele clasice vor rămâne relevante datorită infrastructurii.", 14 },
                { 54, "Diesel", "Eficiența pe distanțe lungi nu poate fi înlocuită complet.", 14 },
                { 55, "Hibrid", "Soluția ideală de tranziție care oferă ce e mai bun din ambele lumi.", 14 },
                { 56, "Electric", "Viitorul mobilității este 100% cu baterii și emisii zero.", 14 },
                { 57, "Nu știu / Nu am o preferință", "Piața este imprevizibilă, se va adapta oricărui standard nou.", 14 }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Ștergem datele inserate, în caz de rollback
            migrationBuilder.Sql("DELETE FROM QuizOptions WHERE Id BETWEEN 1 AND 57;");
            migrationBuilder.Sql("DELETE FROM QuizQuestions WHERE Id BETWEEN 1 AND 14;");
        }
    }
}
