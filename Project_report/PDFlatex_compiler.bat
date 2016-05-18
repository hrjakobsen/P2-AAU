@echo -------------------------------------------------
@echo START 	Compiling PDFLATEX of Document [1/2]
@echo -------------------------------------------------
@echo off
pdflatex DAT2-A423_Project_Report.tex -quiet
@echo -------------------------------------------------
@echo END 	Compiling PDFLATEX of Document [1/2]
@echo -------------------------------------------------
@echo.
@echo. 
@echo -------------------------------------------------
@echo START	Compiling BibTex
@echo -------------------------------------------------
@echo off
bibtex DAT2-A423_Project_Report -quiet >nul
@echo -------------------------------------------------
@echo END 	Compiling BibTex
@echo -------------------------------------------------
@echo. 
@echo. 
@echo -------------------------------------------------
@echo START 	Compiling Index
@echo -------------------------------------------------
@echo off
makeindex DAT2-A423_Project_Report -q >nul
@echo -------------------------------------------------
@echo END 	Compiling Index
@echo -------------------------------------------------
@echo. 
@echo. 
@echo -------------------------------------------------
@echo START 	Compiling PDFLATEX of Document [2/2]
@echo -------------------------------------------------
@echo off
pdflatex DAT2-A423_Project_Report.tex -quiet
@echo -------------------------------------------------
@echo END 	Compiling PDFLATEX of Document [2/2]
@echo -------------------------------------------------
@echo off
del *.aux /S /Q /F >nul 2>nul
del *.toc /S /Q /F >nul 2>nul
del *.log /S /Q /F >nul 2>nul
del *.bbl /S /Q /F >nul 2>nul
del *.blg /S /Q /F >nul 2>nul
del *.brf /S /Q /F >nul 2>nul
del *.out /S /Q /F >nul 2>nul
del *.thm /S /Q /F >nul 2>nul
del *.tdo /S /Q /F >nul 2>nul
del *.xml /S /Q /F >nul 2>nul
del *.cb2 /S /Q /F >nul 2>nul
del *.idx /S /Q /F >nul 2>nul
del *.ilg /S /Q /F >nul 2>nul
del *.ind /S /Q /F >nul 2>nul
del *.cb /S /Q /F >nul 2>nul
del *blx.bib /S /Q /F >nul 2>nul
@echo. 
@echo. 
@echo -------------------------------------------------
@echo 		Cleanup Done!
@echo -------------------------------------------------
PAUSE