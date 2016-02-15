@echo -------------------------------------------------
@echo START 	Compiling PDFLATEX of Document [1/3]
@echo -------------------------------------------------
pdflatex DAT2-A423_Project_Report.tex -quiet
@echo -------------------------------------------------
@echo END 	Compiling PDFLATEX of Document [1/3]
@echo -------------------------------------------------
@echo START	Compiling BibTex
@echo -------------------------------------------------
bibtex DAT2-A423_Project_Report -quiet
@echo -------------------------------------------------
@echo END 	Compiling BibTex
@echo -------------------------------------------------
@echo START 	Compiling PDFLATEX of Document [2/3]
@echo -------------------------------------------------
pdflatex DAT2-A423_Project_Report.tex -quiet
@echo -------------------------------------------------
@echo END 	Compiling PDFLATEX of Document [2/3]
@echo -------------------------------------------------
@echo START 	Compiling PDFLATEX of Document [3/3]
@echo -------------------------------------------------
pdflatex DAT2-A423_Project_Report.tex -quiet
@echo -------------------------------------------------
@echo END 	Compiling PDFLATEX of Document [3/3]
@echo -------------------------------------------------
@echo 		Cleaning up useless files
@echo -------------------------------------------------
@echo off
del *.aux /S /Q /F
del *.toc /S /Q /F
del *.log /S /Q /F
del *.bbl /S /Q /F
del *.blg /S /Q /F
del *.brf /S /Q /F
del *.out /S /Q /F
del *.thm /S /Q /F
del *.tdo /S /Q /F
del *.xml /S /Q /F
@echo -------------------------------------------------
@echo 		Cleanup Done!
@echo -------------------------------------------------
PAUSE