-- possible scenarios for views
-- result table includes all details about a crop cycle
CREATE OR ALTER VIEW CropCycleSummaryView
AS
SELECT * 
FROM
    crop_cycle_data AS cc
	JOIN user_data AS ud ON cc.cultivator = ud.nic
	JOIN environment_data AS ed ON cc.environment = ed.environment_data_id
    JOIN field_data AS fd ON ed.field = fd.field_id
    JOIN fertilizer_data AS fert ON fd.fertilizer = fert.fertilizer_id
    JOIN pest_disease_data AS pd ON cc.pest_disease = pd.pest_disease_id
    JOIN soil_texture_data AS std ON fd.soil_texture = std.soil_texture_id
WHERE
    cc.crop = 1000;
