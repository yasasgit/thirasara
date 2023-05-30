-- possible scenarios for views
-- result table includes all details about a crop cycle
CREATE OR ALTER VIEW CropCycleAllView
AS
SELECT * 
FROM
    crop_cycle_data AS cc
	JOIN crop_data AS cd ON cc.crop = cd.crop_id 
    JOIN environment_data AS ed ON cc.environment = ed.environment_data_id
		JOIN field_data AS fd ON ed.field = fd.field_id
			JOIN soil_texture_data AS std ON fd.soil_texture = std.soil_texture_id
			JOIN user_data AS ud ON fd.cultivator = ud.nic
    JOIN fertilizer_data AS fert ON cc.fertilizer = fert.fertilizer_id
    JOIN crop_cycle_pest_disease AS ccpd ON cc.crop_cycle_id = ccpd.crop_cycle
		JOIN pest_disease_data AS pdd ON ccpd.pest_disease = pdd.pest_disease_id
WHERE
    cc.crop = 1000;
