INSERT INTO user_data (email, first_name, last_name, phone_number, password_hashed, account_type)
VALUES
('yasas99@outlook.com', 'Yasas', 'Harshana', '0761916565', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'officer'),
('shaveen.mod@gmail.com', 'Shaveen', 'Maleesha', '0987654321', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
('noob.wima@gmail.com', 'Wimukthi', 'Dulaj', '5678901234', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'officer'),
('stephan.chad@gmail.com', 'Stephan', 'Fernando', '4321098765', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'officer'),
('uditha.biyeah@live.com', 'Uditha', 'Sampath', '1234567890', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
('ice.namidu@tiktok.com', 'Namindu', 'Hasalanka', '9876543210', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
('ravindu@yahoo.com', 'Ravindu', 'Silva', '4567890123', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
('amali@outlook.com', 'Amali', 'Jayawardena', '7890123456', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
('nadeesha@slt.lk', 'Nadeesha', 'Ratnayake', '2345678901', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator'),
('tharindu@gmail.com', 'Tharindu', 'Gamage', '8901234567', 0x5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8, 'cultivator');

INSERT INTO crop_data (crop_name, genus, species, suitable_temperature_c, suitable_rainfall_irrigation_mm, suitable_humidity_percentage, suitable_wind_speed_m_s, suitable_sunlight_exposure_h_day, suitable_plant_density_ha, suitable_soil_ph, suitable_nitrogen_kg_ha, suitable_phosphorus_kg_ha, suitable_potassium_kg_ha, suitable_soil_texture, feasible_yield_kg_ha, demand_per_crop_cycle_kg, supply_per_crop_cycle_kg)
VALUES 
('Rice', 'Oryza', 'sativa', '20-35', '1000-2500', '70-90', '0.5-2.5', '6-8', '200-400', '5.5-7.0', 150, 60, 150, '3-8', '4000-6000', 5000, 3000);

INSERT INTO fertilizer_data (fertilizer_name, fertilizer_nitrogen_kg_ha, fertilizer_phosphorus_kg_ha, fertilizer_potassium_kg_ha, other_nutrients_kg_ha)
VALUES
('Urea', 100, 0, 0, 0),
('DAP', 18, 46, 0, 0),
('Potassium chloride', 0, 0, 60, 0),
('Ammonium sulfate', 21, 0, 0, 0),
('Single superphosphate', 0, 18, 0, 0),
('Triple superphosphate', 0, 46, 0, 0),
('Ammonium nitrate', 33, 0, 0, 0),
('Ammonium phosphate sulfate', 15, 15, 0, 0),
('Calcium ammonium nitrate', 27, 0, 0, 0),
('Urea ammonium nitrate', 32, 0, 0, 0);

INSERT INTO soil_texture_data (soil_texture_name, soil_texture_level)
VALUES
    ('Clay Soil', 1),
    ('Silty Clay', 2),
    ('Silty Clay Loam', 3),
    ('Clayey Silt', 4),
    ('Sandy Clay Loam', 5),
    ('Sandy Clay', 6),
    ('Silty Loam', 7),
    ('Clay Loam', 8),
    ('Loam Soil', 9),
    ('Sandy Loam', 10),
    ('Silt Soil', 11),
    ('Sandy Soil', 12);

INSERT INTO crop_cycle_pest_disease (crop_cycle, pest_disease)
VALUES
    (1020, 1003), (1020, 1000), 
    (1021, 1006), (1021, 1002), (1021, 1008), 
    (1022, 1007), (1022, 1005),  
    (1023, 1004), (1023, 1001), (1023, 1002), (1023, 1008), (1023, 1003), 
    (1024, 1001), (1024, 1009), 
    (1025, 1002), (1025, 1001), (1025, 1003), (1025, 1005), 
    (1026, 1004), (1026, 1005), (1026, 1002),
    (1027, 1005), 
    (1028, 1006), (1028, 1007), (1028, 1008),
    (1029, 1009), (1029, 1010),
    (1030, 1003), (1030, 1004),(1030, 1006), (1030, 1000), 
    (1031, 1001), (1031, 1009), 
    (1032, 1008), (1032, 1007),
    (1033, 1010), (1033, 1007),(1033, 1001), (1033, 1005);