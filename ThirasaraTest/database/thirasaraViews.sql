-- possible scenarios for views
-- result table includes all details about a crop cycle
CREATE OR ALTER VIEW CropCycleAllView
AS
SELECT * FROM 
crop_cycle_data AS ccd 
JOIN crop_data AS cd ON ccd.crop = cd.crop_id 
JOIN fertilizer_data AS fert ON ccd.fertilizer = fert.fertilizer_id 
JOIN environment_data AS ed ON ccd.environment = ed.environment_data_id 
	JOIN field_data AS fd ON ed.field = fd.field_id
		JOIN user_data AS ud ON fd.cultivator = ud.nic
		JOIN soil_texture_data AS std ON fd.soil_texture = std.soil_texture_id 
JOIN crop_cycle_pest_disease AS ccpd ON ccd.crop_cycle_id = ccpd.crop_cycle
	JOIN pest_disease_data AS pd ON pd.pest_disease_id = ccpd.pest_disease
