using Faahi.Model.im_products;
using Faahi.Service.im_products.im_tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.im_products.Tags
{
    [Route("api/[controller]")]
    [ApiController]
    public class im_tagsController : ControllerBase
    {
        private readonly Iim_tags _im_tags;

        public im_tagsController(Iim_tags im_tags)
        {
            _im_tags = im_tags;
        }

        [Authorize]
        [HttpPost]
        [Route("create_tags")]
        public async Task<ActionResult<im_products_tag>> Create_tags(im_products_tag im_products_tag)
        {
            if (im_products_tag == null)
            {
                return Ok("no data found");
            }
            var created = await _im_tags.Create_tagsAsync(im_products_tag);
            return Ok(created);
        }
        [Authorize]
        [HttpGet]
        [Route("tag_list")]
        public async Task<IActionResult> Tag_List()
        {
            var tage = await _im_tags.Tag_List();
            return Ok(tage);
        }
        [Authorize]
        [HttpGet]
        [Route("tags/{tag_id}")]
        public async Task<IActionResult> Tag_id(string tag_id)
        {
            if(tag_id == null)
            {
                return Ok("no data found");
            }
            var tag = await _im_tags.Tag_id(tag_id);
            return Ok(tag);
        }
        [Authorize]
        [HttpPost]
        [Route("update/{tag_id}")]
        public async Task<ActionResult<im_products_tag>> Update(im_products_tag im_products_tags,string tag_id)
        {
            if(im_products_tags == null)
            {
                return Ok("no data found");
            }
            var upadted_tage =await _im_tags.Update(im_products_tags,tag_id);
            return Ok(upadted_tage);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete/{tag_id}")]
        public async Task<IActionResult> Delete(string tag_id)
        {
            if (tag_id == null)
            {
                return Ok("not found");
            }
            var deleted_tag = await _im_tags.Delete(tag_id);
            return Ok(deleted_tag);
        }


        [Authorize]
        [HttpPost]
        [Route("create_uom")]
        public async Task<ActionResult<im_UnitsOfMeasure>> Create_uom(im_UnitsOfMeasure im_UnitsOfMeasure)
        {
            if (im_UnitsOfMeasure == null)
            {
                return Ok("no data found");
            }
            var created = await _im_tags.Create_umoAsync(im_UnitsOfMeasure);
            return Ok(created);
        }
        [Authorize]
        [HttpGet]
        [Route("uom_list")]
        public async Task<IActionResult> Uom_List()
        {
            var umo = await _im_tags.Uom_List();
            return Ok(umo);
        }
        [Authorize]
        [HttpGet]
        [Route("uom/{uom_id}")]
        public async Task<IActionResult> uom_id(string uom_id)
        {
            if (uom_id == null)
            {
                return Ok("no data found");
            }
            var umo = await _im_tags.uom_id(uom_id);
            return Ok(umo);
        }
        [Authorize]
        [HttpPost]
        [Route("update_uom/{uom_id}")]
        public async Task<ActionResult<im_products_tag>> Update_uom(im_UnitsOfMeasure im_UnitsOfMeasure, string uom_id)
        {
            if (im_UnitsOfMeasure == null)
            {
                return Ok("no data found");
            }
            var upadted_umo = await _im_tags.Update_uom(im_UnitsOfMeasure, uom_id);
            return Ok(upadted_umo);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete_uom/{uom_id}")]
        public async Task<IActionResult> Delete_umo(string uom_id)
        {
            if (uom_id == null)
            {
                return Ok("not found");
            }
            var deleted_umo = await _im_tags.Delete_umo(uom_id);
            return Ok(deleted_umo);
        }
    }
}
