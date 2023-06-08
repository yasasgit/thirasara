CREATE DATABASE thirasara_test_db;

USE thirasara_test_db;

CREATE TABLE user_data
(
    nic VARCHAR(10) PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    phone_number BIGINT,
    password_hashed BINARY(20) NOT NULL,
    account_type VARCHAR(10) NOT NULL CHECK (account_type IN ('cultivator', 'officer'))
);
-- Passwords will be hashed using SHA-1 algorithm

CREATE TABLE crop_data
(
    crop_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
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
    suitable_nitrogen_kg_ha DECIMAL(10, 2),
    suitable_phosphorus_kg_ha DECIMAL(10, 2),
    suitable_potassium_kg_ha DECIMAL(10, 2),
    suitable_soil_texture VARCHAR(20),
    feasible_yield_kg_ha VARCHAR(20),
    demand_per_crop_cycle_kg SMALLINT,
    supply_per_crop_cycle_kg SMALLINT
);
-- Required factors are stored in ranges and assumed to remain the same during each stage of the crop
-- Currently, this is only implemented for rice crops
-- Farmers use the required amounts of fertilizer
-- Added amounts and suggested required amounts are displayed in the farmer dashboard

CREATE TABLE fertilizer_data
(
    fertilizer_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
    fertilizer_name VARCHAR(255) NOT NULL,
    fertilizer_nitrogen_kg_ha DECIMAL(10, 2),
    fertilizer_phosphorus_kg_ha DECIMAL(10, 2),
    fertilizer_potassium_kg_ha DECIMAL(10, 2),
    other_nutrients_kg_ha DECIMAL(10, 2)
);

CREATE TABLE soil_texture_data
(
    soil_texture_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
    soil_texture_name VARCHAR(50) NOT NULL,
    soil_texture_level TINYINT NOT NULL
);

CREATE TABLE pest_disease_data
(
    pest_disease_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
    pest_disease_name VARCHAR(255) NOT NULL,
    vulnerable_crops VARCHAR(255),
    severity_level TINYINT
);

CREATE TABLE field_data
(
    field_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
    cultivator VARCHAR(10),
    size_ha DECIMAL(10, 2) NOT NULL,
    field_location VARCHAR(255),
    soil_nitrogen_kg_ha TINYINT,
    soil_phosphorus_kg_ha TINYINT,
    soil_potassium_kg_ha TINYINT,
    soil_ph DECIMAL(10, 2),
    soil_texture SMALLINT,
    FOREIGN KEY (cultivator) REFERENCES user_data (nic),
    FOREIGN KEY (soil_texture) REFERENCES soil_texture_data (soil_texture_id)
);
-- Each field is assumed to be used by a single farmer (cultivator) at a time
-- Currently, only locations in Homagama are supported
-- The cultivator account type must be "cultivator"

CREATE TABLE crop_cycle_data
(
    crop_cycle_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
    crop SMALLINT,
    field SMALLINT,
    plant_density_ha INT,
    human_hours_ha DECIMAL(10, 2),
    fertilizer SMALLINT,
    fertilizer_kg_ha SMALLINT,
    predicted_yield_kg_ha INT,
    planted_date DATE,
    harvest_date DATE,
    yield_kg_ha SMALLINT,
    FOREIGN KEY (field) REFERENCES field_data (field_id),
    FOREIGN KEY (fertilizer) REFERENCES fertilizer_data (fertilizer_id),
    FOREIGN KEY (crop) REFERENCES crop_data (crop_id)
);
-- Only environmental factors change during a single crop cycle
-- Farmers use only one type of fertilizer during a single crop cycle
-- The predicted yield is based on selected factors

CREATE TABLE crop_cycle_pest_disease
(
    data_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
    crop_cycle SMALLINT,
    pest_disease SMALLINT,
    FOREIGN KEY (crop_cycle) REFERENCES crop_cycle_data (crop_cycle_id),
    FOREIGN KEY (pest_disease) REFERENCES pest_disease_data (pest_disease_id)
);

CREATE TABLE environment_data
(
    environment_data_id SMALLINT IDENTITY(1000, 1) PRIMARY KEY,
    crop_cycle SMALLINT,
    temperature_c DECIMAL(10, 2),
    rainfall_irrigation_mm SMALLINT,
    humidity_perc INT,
    wind_speed_m_s DECIMAL(10, 2),
    sunlight_exposure_h_day DECIMAL(10, 2),
    update_date DATE,
    FOREIGN KEY (crop_cycle) REFERENCES crop_cycle_data (crop_cycle_id)
);
-- When a new record is inserted, the mean value of all records will be taken
-- The values of the latest entry will be used for calculations
-- The entire field is assumed to have the same environment
-- Each environment factor does not exceed practical values
