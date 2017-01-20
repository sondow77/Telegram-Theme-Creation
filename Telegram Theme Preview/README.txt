colors.tdesktop-theme: have all default variables from default.tdesktop-theme of Telegram.
The variables that calls another variable are replaced with the value of the called variable.

For example:
windowBg: #ffffff;
lightButtonBg: windowBg;

It's replaced with:
windowBg: #ffffff;
lightButtonBg: #ffffff;

The converters works only with variables that don't calls another variables like the example.

colors.css: is the result of conversion from colors.tdesktop-theme.
Have all names parsed, and replace the Over, Ripple, Active, Inactive to CSS .hover .active .focus .inactive.
The properties colors are selected by the parsed names, if name contains Bg is a background-color if name contains Fg is color.
The colors that are HEX with Alpha channel are converted to RGBA, and the HEX without Alpha channel isn't converted.

style.css: this file have all styles to make the design sizes and positions to make preview on browser.

Telegram theme.html: this file loads the two css to load the styles for the design.

Convert C to CSS.bat: this file is for run the converter from colors.tdesktop-theme to colors.css.

Convert CSS to C.bat: this file is for run the converter from colors.css to colors.tdesktop-theme.

ctocss.exe: is the converter from colors.tdesktop-theme to CSS, need two arguments, input and output files.

csstoc.exe: is the converter from CSS to colors.tdesktop-theme, need two arguments, input and output files.

images: contains the images extracted from Telegram Source Code and edited to show with HTML and CSS and make the images colored.

OpenSans-Bold.ttf OpenSans-Regular.ttf OpenSans-Semibold.ttf: these files are the fonts that have Telegram on the Source Code.