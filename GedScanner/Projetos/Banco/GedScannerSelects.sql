use GedScannerTwain;

--====================================================================================================================--
--Plano de contas atual
--====================================================================================================================--
if (exists (select * from PlanoConta where PLAN_FECHADO = 0))
begin
	select PLAN_IND,  
		   PLAN_DESCRICAO, 
		   PLAN_CODIGO, 
		   PLAN_FECHADO, 
		   PLAN_DT_INICIO, 
   		   PLAN_LOGININSERT, 
		   PLAN_HISTORICO, 
		   PLAN_ANTERIOR 
	from PlanoConta 
	where PLAN_FECHADO = 0 
end
else begin
	select 0  as PLAN_IND,  
		   '' as PLAN_DESCRICAO, 
		   PLAN_CODIGO, 
		   1  as PLAN_FECHADO, 
		   '' as PLAN_DT_INICIO, 
   		   0  as PLAN_LOGININSERT, 
		   0  as PLAN_HISTORICO, 
		   0  as PLAN_ANTERIOR 
	from PlanoConta 
	order by CONVERT(int, PLAN_CODIGO) desc
end


--====================================================================================================================--
--Set edit plano de contas
--====================================================================================================================--
SET DATEFORMAT DMY  
DECLARE @IND_FEC INT,  
		@IND_ABR INT,  
		@PCONT_CONTA INT, 
		@PCONT_DT_INICIO DATETIME, 
		@PCONT_LOGININSERT INT, 
		@PCONT_HISTORICO INT, 
		@PCONT_ANTERIOR INT 
SET @IND_FEC = (SELECT PLAN_IND FROM PlanoConta WHERE PLAN_FECHADO = 0)   
  
UPDATE PlanoConta 
SET PLAN_FECHADO = 1
WHERE PLAN_IND = @IND_FEC  
  
INSERT INTO PlanoConta (PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT, PLAN_HISTORICO, PLAN_ANTERIOR) 
VALUES ('@PLAN_DESCRICAO', '@PLAN_CODIGO', @PLAN_FECHADO, '@PLAN_DT_INICIO', @LOGIN, 
(SELECT HIST_IND FROM Historico WHERE HIST_DESCRICAO = 'Update'), @IND_FEC) 

SET @IND_ABR = (select IDENT_CURRENT('PlanoConta'))
    
DECLARE PLANOCONTAS_COPIA CURSOR FOR   
	SELECT PCONT_CONTA, PCONT_DT_INICIO, PCONT_LOGININSERT, PCONT_HISTORICO, PCONT_IND FROM PlanContConta 
	WHERE PCONT_PLANOCONTA = @IND_FEC;  
   
OPEN PLANOCONTAS_COPIA 
FETCH NEXT FROM PLANOCONTAS_COPIA  
INTO @PCONT_CONTA, @PCONT_DT_INICIO, @PCONT_LOGININSERT, @PCONT_HISTORICO, @PCONT_ANTERIOR 
   
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		INSERT INTO PlanContConta (PCONT_CONTA, PCONT_PLANOCONTA, PCONT_DT_INICIO, PCONT_LOGININSERT, PCONT_HISTORICO, PCONT_ANTERIOR)  
							VALUES (@PCONT_CONTA, @IND_ABR, @PCONT_DT_INICIO, @PCONT_LOGININSERT, @PCONT_HISTORICO, @PCONT_ANTERIOR) 
   
		FETCH NEXT FROM PLANOCONTAS_COPIA  
		INTO @PCONT_CONTA, @PCONT_DT_INICIO, @PCONT_LOGININSERT, @PCONT_HISTORICO, @PCONT_ANTERIOR 
	END 
CLOSE PLANOCONTAS_COPIA;   
DEALLOCATE PLANOCONTAS_COPIA;   


--====================================================================================================================--
--Set insert plano de contas
--====================================================================================================================--
SET DATEFORMAT DMY 
INSERT INTO PlanoConta (PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT, PLAN_HISTORICO) 
				VALUES ('@PLAN_DESCRICAO', '@PLAN_CODIGO', 0, '@PLAN_DT_INICIO', @LOGIN, 
				(SELECT HIST_IND FROM Historico WHERE HIST_DESCRICAO = 'Update')) 

