-- Criar o banco de dados
CREATE DATABASE IF NOT EXISTS VetClinic;

-- Usar o banco de dados criado
USE VetClinic;

-- Tabela para cadastro de animais domésticos
CREATE TABLE IF NOT EXISTS Animal (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome_dono VARCHAR(100),
    contato_dono VARCHAR(20),
    data_nascimento DATE,
    data_ultima_consulta DATE,
    tipo_animal VARCHAR(50),
    raca VARCHAR(50),
    sexo VARCHAR(1),
    peso DECIMAL(5,2),
    data_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    estado ENUM('ativo', 'inativo') DEFAULT 'ativo'
);

-- Tabela para cadastro de produtos
CREATE TABLE IF NOT EXISTS Produto (
    codigo_produto INT AUTO_INCREMENT PRIMARY KEY,
    tipo_produto VARCHAR(50),
    descricao_produto VARCHAR(255),
    quantidade_armazem INT,
    preco_unitario DECIMAL(8,2),
    estado ENUM('ativo', 'inativo') DEFAULT 'ativo'
);

-- Tabela para registro de atos médicos
CREATE TABLE IF NOT EXISTS AtosMedicos (
    id_ato_medico INT AUTO_INCREMENT PRIMARY KEY,
    ato_medico VARCHAR(100),
    descricao_ato_medico TEXT,
    custo_unitario DECIMAL(8,2),
    data_insercao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    data_ultima_alteracao TIMESTAMP,
    estado ENUM('ativo', 'inativo') DEFAULT 'ativo'
);

-- Tabela para registro de ficha médica do animal
CREATE TABLE IF NOT EXISTS FichaMedica (
    id_ficha_medica INT AUTO_INCREMENT PRIMARY KEY,
    id_animal INT,
    data_ato_medico DATE,
    tipo_consulta VARCHAR(100),
    codigo_colaborador INT,
    diagnostico TEXT,
    ato_medico INT,
    peso DECIMAL(5,2),
    observacoes TEXT,
    prescricao_medica TEXT,
    proxima_visita DATE,
    estado ENUM('ativo', 'inativo') DEFAULT 'ativo',
    FOREIGN KEY (id_animal) REFERENCES Animal(id),
    FOREIGN KEY (ato_medico) REFERENCES AtosMedicos(id_ato_medico)
);

-- Tabela para registrar os materiais utilizados em cada consulta
CREATE TABLE IF NOT EXISTS MateriaisUtilizados (
    id_material INT AUTO_INCREMENT PRIMARY KEY,
    id_ficha_medica INT,
    descricao_material VARCHAR(255),
    quantidade INT,
    FOREIGN KEY (id_ficha_medica) REFERENCES FichaMedica(id_ficha_medica)
);

-- Tabela para registrar os diagnósticos de uma consulta
CREATE TABLE IF NOT EXISTS Diagnosticos (
    id_diagnostico INT AUTO_INCREMENT PRIMARY KEY,
    id_ficha_medica INT,
    descricao_diagnostico VARCHAR(255),
    FOREIGN KEY (id_ficha_medica) REFERENCES FichaMedica(id_ficha_medica)
);