# windrad_proj_DT_YA_NZ_AS
# Windrad-System – Virtual Engineering Konzept
> **Repository:** `Lukenr1/windrad_proj_DT_YA_NZ_AS`  
> **Projekt-Status:** In Bearbeitung (In progress)

Dieses Repository enthält die 3D-Simulation und das Virtual Engineering Konzept für ein interaktives Windkraft-Visualisierungssystem in Unity.

## 1. Projektübersicht & Zweck
Das System ermöglicht eine realistische Simulation aktueller Windverhältnisse an frei wählbaren Orten weltweit. Hierzu werden meteorologische Live-Daten über das Internet abgerufen und abgeglichen. Die daraus resultierende Stromerzeugung wird durch animierte Windräder und visuelle Statusanzeigen in einer virtuellen 3D-Umgebung dargestellt.

**Zweck dieses READMEs:** Außenstehende können sich durch kurzes Lesen dieser Beschreibung schnell und unkompliziert über alle wichtigen Details, Akzeptanzkriterien und den Funktionsumfang des Projekts informieren.

---

## 2. System-Architektur (Input → Verarbeitung → Output)

Das System verbindet die physische Benutzereingabe mit einer virtuellen Dashboard- und Simulationsumgebung:

| Komponente / Szenario | Eingabe (Input) | Verarbeitung | Ausgabe (Output) / Visualisierung |
| :--- | :--- | :--- | :--- |
| **Szenario: London** | Standort: `London` | Abruf des aktuellen Wetterberichts aus London (z. B. Unwetter/Sturm). | Windrad dreht sich schnell, viel Strom wird erzeugt, eine virtuelle Lampe leuchtet grell. |
| **Szenario: Los Angeles** | Standort: `Los Angeles` | Wetterbericht aus L.A. wird abgeglichen (z. B. sonnig, windstill / wenig Wind). | Windrad dreht sich kaum, die Lampe leuchtet nicht, da kein Strom erzeugt wird. |

---

## 3. Systemregeln & Logik

Das Verhalten der Simulation ist durch folgende Kernregeln definiert:
1. **Regel 1 (Keine Eingabe):** Wenn kein Ort angegeben wird, dreht sich das Windrad nicht.
2. **Regel 2 (Windiges Wetter):** Wenn am angegebenen Ort ein windiges Wetter herrscht, dreht sich das jeweilige Windrad schneller und erzeugt Strom.
3. **Regel 3 (Windstille):** Wenn am angegebenen Ort kein Wind weht, dreht sich das Windrad nicht und es wird kein Strom erzeugt.

---

## 4. Virtual Engineering Anteile (Features)
- **[X] Anzeige (Dashboard / Unity):** Visuelle Benutzeroberfläche und 3D-Ausgabe direkt innerhalb der Unity-Engine.
- **[X] Live-Daten (Internet):** Echtzeitanbindung an Online-Wetterberichte zur dynamischen Steuerung.
- **[X] Simulation:** Physikalisch-visuelle Kopplung zwischen realer Windgeschwindigkeit, Rotordrehung und Energie-Output.

---

## 5. Interaktion (Tag der offenen Tür - TdoT)
Besucher des Systems können interaktiv ihren eigenen Heimatort oder einen beliebigen aktuellen Standort in die Anwendung eingeben. Das System prüft die Live-Daten und zeigt dem Besucher direkt im 3D-Gelände an, wie viel Strom das Windrad dort zum aktuellen Zeitpunkt erzeugen würde.

---

## 6. Projekt-Komponenten & Assets

### 3D-Modelle
- **Windrad-Modell:** Ein visuell passendes 3D-Asset, welches sich ordnungsgemäß in Unity öffnen, platzieren und über Scripte animieren lässt.
- **Gelände (Terrain):** Eine texturierte Wiesenlandschaft zur realistischen Einbettung der Windkraftanlagen und der dazugehörigen Infrastruktur (z.B. der Statuslampe).

### Dokumentation & Skizzen
- **Dokumentations-Issue (#1):** Enthält alle grundlegenden Beschreibungen, Skizzen für die Windräder, Skizzen für die Umgebung sowie den logischen Datenfluss.

---

## 7. Projekt-Gruppe & Mitwirkende
* **Projektleitung / Owner:** LukeNr1
* **Entwicklungsteam (Gruppe):** Darko Tepic, Noah Zopf, Alichan Schajchiev, Yohannes Alemu (`yohannes07`)
