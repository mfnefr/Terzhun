VAR questAccepted = false
VAR questCompleted = false
VAR haveCat = false

-> rozcesti

== rozcesti ==
{questCompleted: -> hotovo | {questAccepted: -> mid_quest | -> zacatek}}

== zacatek ==
Vítej, pacholku, v naší vsi. 
Smím tě požádat o malou prosbu? Ztratila se mi kočka a nemohu ji najít. Pomohl bys mi s tím?

+ [1. Ano, není problém, starče.] -> ukol_kocka
+ [2. Nemám čas na takové prkotiny.]

Chápu. Kdyby sis našel čas, tak budu stále tady. Hodně štěstí na stezce, cizinče. -> END

== ukol_kocka ==
~ questAccepted = true

Díky moc, cizinče. Naposledy jsem svou kočku viděl na východ od naší dědiny.

-> END

== mid_quest ==
Tak co? Přinášíš mi dobré zprávy hochu?

+ {haveCat} [Ano, přináším. Zde je tvá kočka. Příště na ni dávej větší pozor, starče.] -> konec
+ [Bohužel zatím nic nemám. Dej mi ještě chvíli.]

Dobrá. Věřím, že mi ji dokážeš pomoci najít. -> END

== konec ==
~ questCompleted = true
Jářku, Micko, co jsi to zase natropila za trable. Doufám, že tady mladíkovi poděkuješ za záchranu. A když ne ty, tak já ano. Děkuji ti, příteli. Pokud budeš něco potřebovati, rád se ti odvděčím. Sbohem.
-> END

== hotovo ==
Zdravím! Micka dostala vynadáno a už se z domu nehne. Ještě jednou díky! -> END