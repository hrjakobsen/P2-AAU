##################################################################
# Makefile for LaTeX
##################################################################
# Use:
# make
# make clean
# options for ps2pdf: http://pages.cs.wisc.edu/~ghost/doc/AFPL/6.50/Ps2pdf.htm

TEX:=$(shell ls *.tex)
OTHER = *~ *.aux *.dvi *.toc *.bbl *.blg *.gz *.out *.thm *.ps *.idx *.ilg *.ind *blx.bib *.log *.xml *.cb*

pdflatex: DAT2-A423_Project_Report.tex
	pdflatex --synctex=1 DAT2-A423_Project_Report.tex
	bibtex DAT2-A423_Project_Report.aux
	pdflatex --synctex=1 DAT2-A423_Project_Report.tex
	pdflatex --synctex=1 DAT2-A423_Project_Report.tex
	rm -f $(OTHER) $(PS)
clean:
	rm -f $(OTHER) $(PS)
