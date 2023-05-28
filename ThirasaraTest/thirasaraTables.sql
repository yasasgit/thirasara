CREATE DATABASE thirasara_db;

USE thirasara_db;

CREATE TABLE crop_data (
    crop_id INT IDENTITY(1000, 1) PRIMARY KEY,
    crop_name VARCHAR(255) NOT NULL,
    genus VARCHAR(255),
    species VARCHAR(255),
    suitable_temperature_c VARCHAR(20),
    suitable_rainfall_irrigation_mm VARCHAR(20),
    suitable_humidity_percentage VARCHAR(20),
    suitable_wind_speed_m_s VARCHAR(20),
    suitable_sunlight_exposure_h_day VARCHAR(20),
    suitable_plant_density_ha VARCHAR(20),
    suitable_soil_ph VARCHAR(20),
    suitable_soil_nitrogen_kg_ha VARCHAR(20),
	suitable_soil_phosphorus_kg_ha VARCHAR(20),
	suitable_soil_potassium_kg_ha VARCHAR(20),
    suitable_soil_texture VARCHAR(20),
	feasible_yield_kg_ha VARCHAR(20),
    demand_per_crop_cycle_kg INT,
	supply_per_crop_cycle_kg INT
);
-- suitable factors in ranges
-- required factors remain same suring each stage of the crop 
-- only rice

CREATE TABLE user_data (
    nic INT IDENTITY(1000, 1) PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    phone_number VARCHAR(20),
    password_hashed BINARY(20) NOT NULL,
    account_type VARCHAR(10) NOT NULL CHECK (account_type IN ('cultivator', 'officer'))
);
-- integer nic
-- sha1 password hashing

CREATE TABLE fertilizer_data (
    fertilizer_id INT IDENTITY(1000, 1) PRIMARY KEY,
	fertilizer_name VARCHAR(255) NOT NULL,
	fertilizer_nitrogen_kg_ha DECIMAL(10, 2),
    fertilizer_phosphorus_kg_ha DECIMAL(10, 2),
    fertilizer_potassium_kg_ha DECIMAL(10, 2),
	other_nutrients_kg_ha DECIMAL(10, 2)
);

CREATE TABLE soil_texture_data (
    soil_texture_id INT IDENTITY(1000, 1) PRIMARY KEY,
    soil_texture_name VARCHAR(50) NOT NULL,
    soil_texture_level INT NOT NULL
);

CREATE TABLE field_data (
    field_id INT IDENTITY(1000, 1) PRIMARY KEY,
    land_owner INT NOT NULL,
    size_ha DECIMAL(10, 2) NOT NULL,
    field_location VARCHAR(255) NOT NULL,
	fertilizer INT NOT NULL,
    soil_nitrogen_kg_ha DECIMAL(10, 2),
    soil_phosphorus_kg_ha DECIMAL(10, 2),
    soil_potassium_kg_ha DECIMAL(10, 2),
	fertilizer_kg_ha DECIMAL(10, 2),
	soil_ph DECIMAL(10, 2),
    soil_texture INT,
	FOREIGN KEY (land_owner) REFERENCES user_data(nic),
	FOREIGN KEY (fertilizer) REFERENCES fertilizer_data(fertilizer_id),
	FOREIGN KEY (soil_texture) REFERENCES soil_texture_data(soil_texture_id)
);
-- farmers only use one crop at a time per field
-- farmers only use one fertilizer at a time per field
-- only areas in homagama

CREATE TABLE environment_data (
    environment_data_id INT IDENTITY(1000, 1) PRIMARY KEY,
	field INT NOT NULL,
    temperature_c DECIMAL(10, 2),
    rainfall_irrigation_mm DECIMAL(10, 2),
    humidity_perc INT,
    wind_speed_m_s DECIMAL(10, 2),
    sunlight_exposure_h_day DECIMAL(10, 2),
	update_date DATETIME,
	FOREIGN KEY (field) REFERENCES field_data(field_id)
);
-- take values of latest entry for calculations
-- whole field has same environment
-- when inserting a new environment data update crop_cycle_data with latest environment
-- each environment factor doesnt exceed..... 

CREATE TABLE pest_disease_data (
    pest_disease_id INT IDENTITY(1000, 1) PRIMARY KEY,
	pest_disease_name VARCHAR(255) NOT NULL,
	vulnerable_crops VARCHAR(255),
	severity_level INT
);

CREATE TABLE crop_cycle_data (
    crop_cycle_id INT IDENTITY(1000, 1) PRIMARY KEY,
	cultivator INT NOT NULL,
	crop INT NOT NULL,
	environment INT NOT NULL,
	planted_date DATE,
	plant_density_ha INT,
	pest_disease INT NOT NULL,
	human_hours_ha DECIMAL(10, 2),
	predicted_yield_kg_ha INT,
    harvest_date DATE,
	yield_kg_ha INT,
	FOREIGN KEY (cultivator) REFERENCES user_data(nic),
	FOREIGN KEY (crop) REFERENCES crop_data(crop_id),
    FOREIGN KEY (environment) REFERENCES environment_data(environment_data_id),
	FOREIGN KEY (pest_disease) REFERENCES pest_disease_data(pest_disease_id)
);
-- should take sum of nutrients (soil + fertilizer)
-- only environment factors change during a crop cycle

INSERT INTO User_Data (email, first_name, last_name, phone_number, password_hashed, account_type)
VALUES
    ('yasas99@outlook.com', 'Yasas', 'Harshana', '0761916565', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'officer'),
    ('person2@example.com', 'Shaveen', 'Maleesha', '0987654321', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
    ('person3@example.com', 'Wimukthi', 'Dulaj', '5678901234', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'officer'),
    ('person4@example.com', 'Stephan', 'Fernando', '4321098765', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'officer'),
    ('email1@example.com', 'Uditha', 'Sampath', '1234567890', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
    ('email2@example.com', 'Namindu', 'Hasalanka', '9876543210', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
    ('email3@example.com', 'Ravindu', 'Silva', '4567890123', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
    ('email4@example.com', 'Amali', 'Jayawardena', '7890123456', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
    ('email5@example.com', 'Nadeesha', 'Ratnayake', '2345678901', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
    ('email6@example.com', 'Tharindu', 'Gamage', '8901234567', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator');

INSERT INTO crop_data (crop_name, genus, species, suitable_temperature_c, suitable_rainfall_irrigation_mm, suitable_humidity_percentage, suitable_wind_speed_m_s, suitable_sunlight_exposure_h_day, suitable_plant_density_ha, suitable_soil_ph, suitable_soil_nitrogen_kg_ha, suitable_soil_phosphorus_kg_ha, suitable_soil_potassium_kg_ha, suitable_soil_texture, feasible_yield_kg_ha, demand_per_crop_cycle_kg, supply_per_crop_cycle_kg)
VALUES 
('Rice', 'Oryza', 'sativa', '20-35', '1500-2500', '70-90', '0.5-2.5', '6-8', '200-400', '5.5-6.5', '80-120', '40-60', '120-200', 'Clayey loam', '4000-6000', 1000, 800);

INSERT INTO fertilizer_data (fertilizer_name, fertilizer_nitrogen_kg_ha, fertilizer_phosphorus_kg_ha, fertilizer_potassium_kg_ha, other_nutrients_kg_ha)
VALUES 
    ('Fertilizer A', 50.25, 20.50, 30.75, 10.00),
    ('Fertilizer B', 40.00, 10.25, 50.00, 15.50),
    ('Fertilizer C', 60.75, 30.00, 20.25, 5.75),
    ('Fertilizer D', 45.50, 25.75, 15.00, 8.25);

INSERT INTO pest_disease_data (pest_disease_name, vulnerable_crops, severity_level)
VALUES 
    ('Aphids', 'Tomatoes, Peppers, Lettuce', 3),
    ('Powdery Mildew', 'Grapes, Apples, Cucumbers', 2),
    ('Fusarium Wilt', 'Bananas, Tomatoes, Watermelons', 4),
    ('Late Blight', 'Potatoes, Tomatoes', 5),
    ('Cabbage Worm', 'Cabbage, Broccoli, Cauliflower', 2);

INSERT INTO soil_texture_data (soil_texture_name, soil_texture_level)
VALUES
    ('Clay Soil', 1),
    ('Silty Clay', 2),
    ('Silty Clay Loam', 3),
    ('Clayey Silt', 4),
    ('Sandy Clay Loam', 5),
    ('Clay Loam', 6),
    ('Sandy Clay', 7),
    ('Silty Loam', 8),
    ('Loam Soil', 9),
    ('Sandy Loam', 10),
    ('Silt Soil', 11),
    ('Sandy Soil', 12);