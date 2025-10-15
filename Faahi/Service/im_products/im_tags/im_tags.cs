using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.im_products;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Service.im_products.im_tags
{
    public class im_tags : Iim_tags
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<im_tags> _logger;
        public im_tags(ApplicationDbContext context, ILogger<im_tags> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<im_products_tag>> Create_tagsAsync(im_products_tag im_Products_Tag)
        {
            if (im_Products_Tag == null)
            {
                _logger.LogError("Create_tagsAsync: No data found");
                return new ServiceResult<im_products_tag>
                {
                    Success = false,
                    Message = "NO data found",
                    Status = -1
                };
            }
            try
            {

                im_Products_Tag.tag_id = Guid.CreateVersion7();
                im_Products_Tag.item_class_id = im_Products_Tag.item_class_id;
                im_Products_Tag.item_subclass_id = im_Products_Tag.item_subclass_id;
                im_Products_Tag.description = im_Products_Tag.description;
                im_Products_Tag.edit_date_time = DateTime.Now;



                _context.im_products_tag.Add(im_Products_Tag);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_products_tag>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = im_Products_Tag
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create_tagsAsync: An error occurred - {ex.Message}");
                return new ServiceResult<im_products_tag>
                {
                    Success = false,
                    Message = "An error occurred while creating the tag.",
                    Status = -1
                };
            }

        }

        public async Task<ServiceResult<List<im_products_tag>>> Tag_List()
        {
            if (_context.im_products_tag == null)
            {
                return new ServiceResult<List<im_products_tag>>
                {
                    Success = false,
                    Message = "no data found",
                    Status = -1,

                };
            }
            var tags = await _context.im_products_tag.ToListAsync();

            return new ServiceResult<List<im_products_tag>>
            {
                Success = true,
                Status = 1,
                Message = "Success",
                Data = tags
            };
        }

        public async Task<ServiceResult<im_products_tag>> Tag_id(string tag_id)
        {
            if (_context.im_products_tag == null)
            {
                return new ServiceResult<im_products_tag>
                {
                    Success = false,
                    Message = "no data found",
                    Status = -1,

                };
            }
            var tag_id_guid = Guid.Parse(tag_id);
            var tags = await _context.im_products_tag.FirstOrDefaultAsync(a => a.tag_id == tag_id_guid);

            return new ServiceResult<im_products_tag>
            {
                Success = true,
                Status = 1,
                Message = "Success",
                Data = tags
            };
        }
        public async Task<ServiceResult<im_products_tag>> Update(im_products_tag im_Products_Tag, string tag_id)
        {
            if (im_Products_Tag == null)
            {
                _logger.LogWarning("Update: No data found");
                return new ServiceResult<im_products_tag>
                {
                    Success = false,
                    Message = "NO data found",
                    Status = -1
                };
            }
            try
            {
                var tag_id_guid = Guid.Parse(tag_id);

                var tages = await _context.im_products_tag.FirstOrDefaultAsync(a => a.tag_id == tag_id_guid);

                tages.item_class_id = im_Products_Tag.item_class_id;
                tages.item_subclass_id = im_Products_Tag.item_subclass_id;
                tages.description = im_Products_Tag.description;
                tages.edit_date_time = DateTime.Now;



                _context.im_products_tag.Update(tages);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_products_tag>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = im_Products_Tag
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update: An error occurred - {ex.Message}");
                return new ServiceResult<im_products_tag>
                {
                    Success = false,
                    Message = "An error occurred while updating the tag.",
                    Status = -1
                };
            }

        }
        public async Task<ServiceResult<im_products_tag>> Delete(string tag_id)
        {
            if (tag_id == null)
            {
                return new ServiceResult<im_products_tag>
                {
                    Success = false,
                    Message = "no dat foudn",
                    Status = -1
                };
            }
            var tag_id_guid = Guid.Parse(tag_id);

            var delete_tag = await _context.im_products_tag.FirstOrDefaultAsync(a => a.tag_id == tag_id_guid);
            if (delete_tag != null)
            {
                _context.im_products_tag.Remove(delete_tag);
                await _context.SaveChangesAsync();
            }
            return new ServiceResult<im_products_tag>
            {
                Success = true,
                Message = "deleted",
                Status = 1
            };
        }

        public async Task<ServiceResult<im_UnitsOfMeasure>> Create_umoAsync(im_UnitsOfMeasure _UnitsOfMeasure)
        {
            if (_UnitsOfMeasure == null)
            {
                _logger.LogError("Create_umoAsync: No data found");
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = false,
                    Message = "NO data found",
                    Status = -1
                };
            }
            try
            {
                _UnitsOfMeasure.uom_id = Guid.CreateVersion7();
                _UnitsOfMeasure.name = _UnitsOfMeasure.name;
                _UnitsOfMeasure.abbreviation = _UnitsOfMeasure.abbreviation;


                _context.im_UnitsOfMeasures.Add(_UnitsOfMeasure);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = _UnitsOfMeasure
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create_umoAsync: An error occurred - {ex.Message}");
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = false,
                    Message = "An error occurred while creating the UOM.",
                    Status = -1
                };
            }
            
        }
        public async Task<ServiceResult<List<im_UnitsOfMeasure>>> Uom_List()
        {
            if (_context.im_products_tag == null)
            {
                return new ServiceResult<List<im_UnitsOfMeasure>>
                {
                    Success = false,
                    Message = "no data found",
                    Status = -1,

                };
            }
            var umo = await _context.im_UnitsOfMeasures.ToListAsync();

            return new ServiceResult<List<im_UnitsOfMeasure>>
            {
                Success = true,
                Status = 1,
                Message = "Success",
                Data = umo
            };
        }
        public async Task<ServiceResult<im_UnitsOfMeasure>> uom_id(string uom_id)
        {
            if (_context.im_products_tag == null)
            {
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = false,
                    Message = "no data found",
                    Status = -1,

                };
            }
            var uom_guid = Guid.Parse(uom_id);

            var umo = await _context.im_UnitsOfMeasures.FirstOrDefaultAsync(a => a.uom_id == uom_guid);

            return new ServiceResult<im_UnitsOfMeasure>
            {
                Success = true,
                Status = 1,
                Message = "Success",
                Data = umo
            };
        }
        public async Task<ServiceResult<im_UnitsOfMeasure>> Update_uom(im_UnitsOfMeasure im_UnitsOfMeasure, string uom_id)
        {
            if (im_UnitsOfMeasure == null)
            {
                _logger.LogWarning("Update_uom: No data found");
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = false,
                    Message = "NO data found",
                    Status = -1
                };
            }
            try
            {
                var uom_guid = Guid.Parse(uom_id);

                var umo = await _context.im_UnitsOfMeasures.FirstOrDefaultAsync(a => a.uom_id == uom_guid);

                umo.name = im_UnitsOfMeasure.name;
                umo.abbreviation = im_UnitsOfMeasure.abbreviation;



                _context.im_UnitsOfMeasures.Update(umo);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = umo
                };
            }
            catch(Exception ex)
            {
                _logger.LogError($"Update_uom: An error occurred - {ex.Message}");
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = false,
                    Message = "An error occurred while updating the UOM.",
                    Status = -1
                };
            }
           
        }
        public async Task<ServiceResult<im_UnitsOfMeasure>> Delete_umo(string uom_id)
        {
            if (uom_id == null)
            {
                return new ServiceResult<im_UnitsOfMeasure>
                {
                    Success = false,
                    Message = "no dat foudn",
                    Status = -1
                };
            }
            var uom_guid = Guid.Parse(uom_id);

            var delete_tag = await _context.im_UnitsOfMeasures.FirstOrDefaultAsync(a => a.uom_id == uom_guid);
            if (delete_tag != null)
            {
                _context.im_UnitsOfMeasures.Remove(delete_tag);
                await _context.SaveChangesAsync();
            }
            return new ServiceResult<im_UnitsOfMeasure>
            {
                Success = true,
                Message = "deleted",
                Status = 1
            };
        }
    }
}
