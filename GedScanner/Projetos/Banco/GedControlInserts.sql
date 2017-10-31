use GedControlDataBase;

update Clientes
set CLI_FISCAL = 245,
	CLI_CONTABIL = 245,
	CLI_PESSOAL = 245
WHERE CLI_IND = 6 OR CLI_IND = 10

insert into ClienteDatabase (BASE_DESCRICAO, BASE_NOMEBANCO, BASE_USUARIO, BASE_SENHA, BASE_SERVER, BASE_CLIENTE) 
values ('Primeiro Banco', 'GedScannerTwain', 'sa', '1234', 'LOCALHOST', 6)