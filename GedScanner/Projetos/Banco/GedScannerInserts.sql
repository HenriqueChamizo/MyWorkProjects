use GedScannerTwain;

--declare @cliente int = 6

--LoginsInternos--
--insert into LoginsInternos
-- (LOGI_NOME, LOGI_CARGO, LOGI_EMAIL, LOGI_TELEFONE, LOGI_SETOR, LOGI_PWD, LOGI_ATIVO, LOGI_SUPERVISOR, 
--  LOGI_HOME_OFFICE, LOGI_AUDITOR, LOGI_DIRETORIA, LOGI_PROGRAMACAO, LOGI_TESTE, LOGI_PRAZO_ACRESCENTAR, 
--  LOGI_RECEBER_AVISO_ALTERACAO_PERFIL)
--values
-- ('Henrique Chamizo', 'Programador', 'henrique@assecont.com.br', '2954-9939', 1, 'qualquer senha', 
--  1, 1, 1, 1, 1, 1, 0, 0, 1)

--PlanoConta--
insert into PlanoConta (PLAN_DESCRICAO, PLAN_CODIGO, PLAN_FECHADO, PLAN_DT_INICIO, PLAN_LOGININSERT) 
values ('Plano Geral', '0000000001', 0, GETDATE(), 245)

--ContaTipo--
insert into ContaTipo (CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT) 
values ('Receita', '1000', GETDATE(), 245)
insert into ContaTipo (CONTP_DESCRICAO, CONTP_CLASSIFICADOR, CONTP_DT_INICIO, CONTP_LOGININSERT) 
values ('Despesa', '2000', GETDATE(), 245)

--Conta--
insert into Conta (CONT_DESCRICAO, CONT_ACESSO, CONT_CONTATIPO, CONT_DT_INICIO, CONT_LOGININSERT) 
values ('Fornecedores', '21101', 
		(select CONTP_IND from ContaTipo
		 where CONTP_CLASSIFICADOR = '2000'), GETDATE(), 245)
insert into Conta (CONT_DESCRICAO, CONT_ACESSO, CONT_CONTATIPO, CONT_DT_INICIO, CONT_LOGININSERT) 
values ('Conta de Luz', '20002', 
		(select CONTP_IND from ContaTipo
		 where CONTP_CLASSIFICADOR = '2000'), GETDATE(), 245)

--PlanContConta--
insert into PlanContConta (PCONT_CONTA, PCONT_PLANOCONTA, PCONT_DT_INICIO, PCONT_LOGININSERT) 
values ((select CONT_IND from Conta where CONT_ACESSO = '21101'), 
		(select PLAN_IND from PlanoConta where PLAN_CODIGO = '0000000001'), GETDATE(), 245)

--GedArquivoTipo--
insert into GedArquivoTipo (GEDTIPO_DESCRICAO, GEDTIPO_EXPORTA, GEDTIPO_DT_INICIO, GEDTIPO_LOGININSERT) 
values ('Boleto', 1, GETDATE(), 245)
insert into GedArquivoTipo (GEDTIPO_DESCRICAO, GEDTIPO_EXPORTA, GEDTIPO_DT_INICIO, GEDTIPO_LOGININSERT) 
values ('DARF', 0, GETDATE(), 245)

--ContArqTipo--
insert into ContArqTipo (CATIP_PLANCONTCONTA, CATIP_ARQUIVOTIPO, CATIP_DT_INICIO, CATIP_LOGININSERT) 
values ((select PCONT_IND from PlanContConta
		 inner join Conta on CONT_IND = PCONT_CONTA 
		 where CONT_ACESSO = '21101'), 
		(select GEDTIPO_IND from GedArquivoTipo where GEDTIPO_DESCRICAO = 'Boleto'), GETDATE(), 245)
insert into ContArqTipo (CATIP_PLANCONTCONTA, CATIP_ARQUIVOTIPO, CATIP_DT_INICIO, CATIP_LOGININSERT) 
values ((select PCONT_IND from PlanContConta
		 inner join Conta on CONT_IND = PCONT_CONTA 
		 where CONT_ACESSO = '21101'), 
		(select GEDTIPO_IND from GedArquivoTipo where GEDTIPO_DESCRICAO = 'DARF'), GETDATE(), 245)
insert into ContArqTipo (CATIP_PLANCONTCONTA, CATIP_ARQUIVOTIPO, CATIP_DT_INICIO, CATIP_LOGININSERT)
values (2, 1, GETDATE(), 255)

--GedCampoTipo-- 
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Texto')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Caixa de Texto')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Inteiro')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Numérico')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Monetário')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Sim/Não')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Data')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Hora')
insert into GedCampoTipo (CAPTIP_DESCRICAO) values ('Data e Hora')

--GedCampo--
insert into GedCampo (CAMP_DESCRICAO, CAMP_ARQUIVOTIPO, CAMP_TIPO, CAMP_DT_INICIO, CAMP_LOGININSERT)
values ('Descrição', 
		(select GEDTIPO_IND from GedArquivoTipo where GEDTIPO_DESCRICAO = 'Boleto'),
		(select CAPTIP_IND from GedCampoTipo where CAPTIP_DESCRICAO = 'Texto'), GETDATE(), 245)
insert into GedCampo (CAMP_DESCRICAO, CAMP_ARQUIVOTIPO, CAMP_TIPO, CAMP_DT_INICIO, CAMP_LOGININSERT)
values ('Valor', 
		(select GEDTIPO_IND from GedArquivoTipo where GEDTIPO_DESCRICAO = 'Boleto'),
		(select CAPTIP_IND from GedCampoTipo where CAPTIP_DESCRICAO = 'Numérico'), GETDATE(), 245)
insert into GedCampo (CAMP_DESCRICAO, CAMP_ARQUIVOTIPO, CAMP_TIPO, CAMP_DT_INICIO, CAMP_LOGININSERT)
values ('Vencimento', 
		(select GEDTIPO_IND from GedArquivoTipo where GEDTIPO_DESCRICAO = 'Boleto'),
		(select CAPTIP_IND from GedCampoTipo where CAPTIP_DESCRICAO = 'Data'), GETDATE(), 245)

--GedArquivo--
insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, 
						GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_CONTARQUIVOTIPO)
values (GETDATE(), 1, 
		(select BulkColumn from Openrowset(Bulk 'D:/coala.jpg', Single_Blob) as img),
		11, 'JPG', 'Pago Duplicata da Polibrasil', 
		(select a.CATIP_IND from ContArqTipo a
		 inner join GedArquivoTipo c on c.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO
		 where c.GEDTIPO_DESCRICAO = 'Boleto'))
insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, 
						GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_CONTARQUIVOTIPO)
values (GETDATE(),  1, 
		(select BulkColumn from Openrowset(Bulk 'D:/xadrez.jpg', Single_Blob) as img),
		11, 'JPG', 'Conta de Luz', 
		(select a.CATIP_IND from ContArqTipo a
		 inner join GedArquivoTipo c on c.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO
		 where c.GEDTIPO_DESCRICAO = 'Boleto'))
insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, 
						GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_CONTARQUIVOTIPO)
values (GETDATE(), 1, 
		(select BulkColumn from Openrowset(Bulk 'D:/lion.jpg', Single_Blob) as img),
		11, 'JPG', 'Compra de Material', 
		(select a.CATIP_IND from ContArqTipo a
		 inner join GedArquivoTipo c on c.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO
		 where c.GEDTIPO_DESCRICAO = 'Boleto'))
insert into GedArquivo (GEDARQ_DISPONIVELEM, GEDARQ_DISPONIVELPOR, GEDARQ_ARQUIVO, 
						GEDARQ_TAMANHO, GEDARQ_EXTENSAO, GEDARQ_DESCRICAO, GEDARQ_CONTARQUIVOTIPO)
values (GETDATE(), 1, 
		(select BulkColumn from Openrowset(Bulk 'D:/iron.jpg', Single_Blob) as img),
		11, 'JPG', 'Conta de Água', 
		(select a.CATIP_IND from ContArqTipo a
		 inner join GedArquivoTipo c on c.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO
		 where c.GEDTIPO_DESCRICAO = 'Boleto'))

--GedCampoValor--
insert into GedCampoValor (CAPVAL_CAMPO, CAPVAL_CONTARQUIVOSTIPO, CAPVAL_VALOR, CAPVAL_ATUAL, CAPVAL_DATA)
values ((select CAMP_IND from GedCampo where CAMP_DESCRICAO = 'Descrição'),
		(select a.CATIP_IND from ContArqTipo a
		 inner join GedArquivoTipo b on b.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO
		 where b.GEDTIPO_DESCRICAO = 'Boleto'), 
		'Conta de Luz', 1, GETDATE())
insert into GedCampoValor (CAPVAL_CAMPO, CAPVAL_CONTARQUIVOSTIPO, CAPVAL_VALOR, CAPVAL_ATUAL, CAPVAL_DATA)
values ((select CAMP_IND from GedCampo where CAMP_DESCRICAO = 'Valor'),
		(select a.CATIP_IND from ContArqTipo a
		 inner join GedArquivoTipo b on b.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO
		 where b.GEDTIPO_DESCRICAO = 'Boleto'), 
		'150,00', 1, GETDATE())
insert into GedCampoValor (CAPVAL_CAMPO, CAPVAL_CONTARQUIVOSTIPO, CAPVAL_VALOR, CAPVAL_ATUAL, CAPVAL_DATA)
values ((select CAMP_IND from GedCampo where CAMP_DESCRICAO = 'Vencimento'),
		(select a.CATIP_IND from ContArqTipo a
		 inner join GedArquivoTipo b on b.GEDTIPO_IND = a.CATIP_ARQUIVOTIPO
		 where b.GEDTIPO_DESCRICAO = 'Boleto'), 
		'23/01/1997', 1, GETDATE())

select * from PlanoConta
select * from ContaTipo
select * from Conta
select * from PlanContConta
select * from GedArquivoTipo
select * from GedCampoTipo
select * from GedCampo
select * from ContArqTipo
select * from GedCampoValor
select * from GedArquivo