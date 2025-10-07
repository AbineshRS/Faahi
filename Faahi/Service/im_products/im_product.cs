using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.Product_dto;
using Faahi.Model.im_products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Faahi.Service.im_products
{
    public class im_product : Iim_products
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _webHostEnvironment;


        public im_product(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        

        public async Task<ServiceResult<im_Products>> Create_Product(im_Products im_Product)
        {
            if (im_Product == null)
            {
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "No data was inserted"
                };
            }
            //var table = "im_Products";
            //var am_table = await _context.am_table_next_key.FindAsync(table);
            //var next_key = Convert.ToInt16(am_table.next_key);

            //var table2 = "im_ProductVariants";
            //var am_table2 = await _context.am_table_next_key.FindAsync(table2);
            //var next_key2 = Convert.ToInt16(am_table2.next_key);

            //var table3 = "im_PriceTiers";
            //var am_table3 = await _context.am_table_next_key.FindAsync(table3);
            //var next_key3 = Convert.ToInt16(am_table3.next_key);

            //var table4 = "im_ProductVariantPrices";
            //var am_table4 = await _context.am_table_next_key.FindAsync(table4);
            //var next_key4 = Convert.ToInt16(am_table4.next_key);

            //var table5 = "im_Product_Subvariants";
            //var am_table5 = await _context.am_table_next_key.FindAsync(table5);
            //var next_key5 = Convert.ToInt16(am_table5.next_key);
            Random rnd = new Random();

            im_Product.product_id = Guid.CreateVersion7();
            im_Product.company_id = im_Product.company_id;
            im_Product.item_class_id = im_Product.item_class_id;
            im_Product.item_subclass_id = im_Product.item_subclass_id;
            im_Product.title = im_Product.title;
            im_Product.description = im_Product.description;
            im_Product.brand = im_Product.brand;
            im_Product.tax_class = im_Product.tax_class;
            im_Product.HS_CODE = rnd.Next(100000, 999999).ToString();
            im_Product.vendor_Code = im_Product.vendor_Code;
            im_Product.created_at = DateTime.Now;
            im_Product.updated_at = DateTime.Now;
            im_Product.dutyP = im_Product.dutyP;
            im_Product.katta = im_Product.katta;
            im_Product.featured_item = im_Product.featured_item;
            im_Product.ignore_direct = im_Product.ignore_direct;
            im_Product.consign_item = im_Product.consign_item;
            im_Product.free_item = im_Product.free_item;
            im_Product.ignore_direct = im_Product.ignore_direct;
            im_Product.restrict_HS = im_Product.restrict_HS;
            im_Product.stock = im_Product.stock;
            im_Product.status = im_Product.status;

            foreach (var im_varint in im_Product.im_ProductVariants)
            {
                im_varint.variant_id = Guid.CreateVersion7();
                im_varint.product_id = im_Product.product_id;
                im_varint.uom_id = im_varint.uom_id;
                var namePart = Regex.Replace(im_Product.title ?? "", @"\s+", "")
                    .Substring(0, Math.Min(3, (im_Product.title ?? "").Length))
                    .ToUpper();
                var SKU = namePart + "-";
                im_varint.sku =SKU + rnd.Next(100000, 999999).ToString();
                im_varint.color = im_varint.color;
                im_varint.size = im_varint.size;
                im_varint.price = im_varint.price;
                im_varint.stock_quantity = im_varint.stock_quantity;
                im_varint.weight_kg = im_varint.weight_kg;
                im_varint.width_cm = im_varint.width_cm;
                im_varint.height_cm = im_varint.height_cm;
                im_varint.length_cm = im_varint.length_cm;
                var chargeable_weight = im_varint.width_cm * im_varint.height_cm * im_varint.length_cm;
                im_varint.chargeable_weight_kg = chargeable_weight/10000;
                im_varint.created_at = DateTime.Now;
                im_varint.updated_at = DateTime.Now;
                im_varint.allow_below_Zero = im_varint.allow_below_Zero;
                im_varint.low_stock_alert = im_varint.low_stock_alert;


                foreach (var sub_varient in im_varint.im_Product_Subvariants)
                {
                    sub_varient.sub_variant_id = Guid.CreateVersion7();
                    sub_varient.variant_id = im_varint.variant_id;
                    sub_varient.product_id = im_Product.product_id;
                    sub_varient.variantType = sub_varient.variantType;
                    sub_varient.variantValue = sub_varient.variantValue;
                    sub_varient.list_price = sub_varient.list_price;
                    sub_varient.standard_cost = sub_varient.standard_cost;
                    sub_varient.last_cost = sub_varient.last_cost;
                    sub_varient.avg_cost = sub_varient.avg_cost;
                    sub_varient.ws_price = sub_varient.ws_price;
                    sub_varient.profit_p = sub_varient.profit_p;
                    sub_varient.minimum_selling = sub_varient.minimum_selling;
                    sub_varient.item_barcode = sub_varient.item_barcode;
                    sub_varient.modal_number = sub_varient.modal_number;
                    sub_varient.shipping_weight = sub_varient.shipping_weight;
                    sub_varient.shipping_length = sub_varient.shipping_length;
                    sub_varient.shipping_width = sub_varient.shipping_width;
                    sub_varient.shipping_height = sub_varient.shipping_height;
                    sub_varient.total_volume = sub_varient.total_volume;
                    sub_varient.total_weight = sub_varient.total_weight;
                    sub_varient.deduct_qnty = sub_varient.deduct_qnty;
                    sub_varient.quantity = sub_varient.quantity;
                    sub_varient.created_at = DateTime.Now;
                    sub_varient.edit_user_id = sub_varient.edit_user_id;
                    sub_varient.unit_breakdown = sub_varient.unit_breakdown;
                    sub_varient.fixed_price = sub_varient.fixed_price;
                    sub_varient.generateBarcode = sub_varient.generateBarcode;
                   

                    foreach (var price_tair in sub_varient.im_PriceTiers)
                    {
                        price_tair.price_tier_id =Guid.CreateVersion7();
                        price_tair.variant_id = im_varint.variant_id;
                        price_tair.name = price_tair.name;
                        price_tair.sub_variant_id = sub_varient.sub_variant_id;
                        price_tair.description = price_tair.description;

                        

                        foreach (var price_varient in price_tair.im_ProductVariantPrices)
                        {
                            price_varient.variant_price_id = Guid.CreateVersion7();
                            price_varient.sub_variant_id = sub_varient.sub_variant_id;
                            price_varient.company_id = im_Product.company_id;
                            price_varient.price_tier_id = price_tair.price_tier_id;
                            price_varient.price = price_varient.price;
                            price_varient.currency = price_varient.currency;
                            price_varient.created_at = DateTime.Now;
                            price_varient.updated_at = DateTime.Now;


                        }
                    }
                }
                

            }
            _context.im_Products.Add(im_Product);

            

            await _context.SaveChangesAsync();

            return new ServiceResult<im_Products>
            {
                Success = true,
                Message = " created successfully",
                Data = im_Product
            };
        }
        public async Task<ActionResult<ServiceResult<string>>> UploadProductAsync(IFormFile formFile, string product_id)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No file uploaded.",
                    Data = null
                };
            }
            FileInfo fileInfo = new FileInfo(formFile.FileName);
            var product_guid = Guid.Parse(product_id);
            var im_product = await _context.im_Products.FirstOrDefaultAsync(a => a.product_id == product_guid);

            var newItemFile = "Item_" + im_product.product_id + "_1" + fileInfo.Extension;
            string relativeFolder = Path.Combine("Images", "ProductItems", im_product.product_id.ToString(), "Default");
            string fullFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, relativeFolder);

            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }

            //delete image
            if (!string.IsNullOrEmpty(im_product.thumbnail_url))
            {
                string oldFullPath = Path.Combine(_webHostEnvironment.WebRootPath, im_product.thumbnail_url.Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (System.IO.File.Exists(oldFullPath))
                {
                    System.IO.File.Delete(oldFullPath);
                }
            }

            string fullPath = Path.Combine(fullFolderPath, newItemFile);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
                await stream.FlushAsync();
            }
            string relativePath = Path.Combine(relativeFolder, newItemFile).Replace("\\", "/");

            im_product.thumbnail_url = relativePath;
            _context.im_Products.Update(im_product);
            await _context.SaveChangesAsync();
            return new ServiceResult<string>
            {
                Success = true,
                Message = "File uploaded",
                Data = relativePath
            };

        }
        public async Task<ActionResult<ServiceResult<string>>> UploadMutiple_image(IFormFile[] formFile, string product_id, string variant_id)
        {
            var table = "im_ProductImages";
            var am_table = await _context.am_table_next_key.FindAsync(table);
            var next_key = Convert.ToInt32(am_table.next_key);

            int itemNumber = 0;
            var guid_varientid= Guid.Parse(variant_id);
            var guid_product_id = Guid.Parse(product_id);
            //var product = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.variant_id == variant_id);
            var im_varient = await _context.im_product_subvariant.FirstOrDefaultAsync(a => a.sub_variant_id == guid_varientid);
            var existingImages = await _context.im_ProductImages.Where(a => a.variant_id == guid_varientid).ToListAsync();

            if (existingImages.Any())
            {
                foreach (var existingImage in existingImages)
                {
                    var fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                        existingImage.image_url.Replace("/", Path.DirectorySeparatorChar.ToString()));

                    if (System.IO.File.Exists(fullImagePath))
                    {
                        System.IO.File.Delete(fullImagePath);
                    }

                    _context.im_ProductImages.Remove(existingImage);
                }
            }

            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "ProductItems", product_id, "ProductSubImages", variant_id);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var newfile in formFile)
            {
                itemNumber++;
                FileInfo fileInfo = new FileInfo(newfile.FileName);
                var newFileName = $"sub_{product_id}_{itemNumber}{fileInfo.Extension}";
                var fullPath = Path.Combine(folderPath, newFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await newfile.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("Images", "ProductItems", product_id, "ProductSubImages", variant_id, newFileName).Replace("\\", "/");
                var image = new im_ProductImages
                {
                    image_id = Guid.CreateVersion7(),
                    product_id = guid_product_id,
                    variant_id = guid_varientid,
                    image_url = relativePath,
                    uploaded_at = DateTime.Now,
                    is_primary = "F",
                    display_order = itemNumber
                };
                _context.im_ProductImages.Add(image);
            }
            am_table.next_key = next_key;
            _context.am_table_next_key.Update(am_table);

            await _context.SaveChangesAsync();

            return new ServiceResult<string>
            {
                Success = true,
                Message = "All files uploaded successfully.",
                Data = null
            };
        }
        public async Task<ActionResult<ServiceResult<string>>> Upload_vedio(IFormFile[] formFile, string product_id, string variant_id)
        {
            var table = "im_ProductImages";
            var am_table = await _context.am_table_next_key.FindAsync(table);
            var next_key = Convert.ToInt32(am_table.next_key);
            var guid_varientid = Guid.Parse(variant_id);
            var guid_product_id = Guid.Parse(product_id);
            int itemNumber = 0;

            var product = await _context.im_product_subvariant.FirstOrDefaultAsync(a => a.sub_variant_id == guid_varientid);
            

            if (product == null)
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "Invalid product variant ID",
                    Data = null
                };
            }

            // Delete existing images (videos in this case)
            // Delete only videos (image_url contains '/video/')
            var existingVideos = await _context.im_ProductImages
                .Where(a => a.variant_id == guid_varientid && a.image_url.Contains("/video/"))
                .ToListAsync();

            if (existingVideos.Any())
            {
                foreach (var video in existingVideos)
                {
                    var fullVideoPath = Path.Combine(_webHostEnvironment.WebRootPath,
                        video.image_url.Replace("/", Path.DirectorySeparatorChar.ToString()));

                    if (System.IO.File.Exists(fullVideoPath))
                    {
                        System.IO.File.Delete(fullVideoPath);
                    }

                    _context.im_ProductImages.Remove(video);
                }
            }


            // Create target folder: .../ProductSubImages/{variant_id}/video/
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "ProductItems", product_id, "ProductSubImages", variant_id, "video");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var newfile in formFile)
            {
                itemNumber++;
                FileInfo fileInfo = new FileInfo(newfile.FileName);
                var newFileName = $"sub_{product_id}_{itemNumber}{fileInfo.Extension}";
                var fullPath = Path.Combine(folderPath, newFileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await newfile.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("Images", "ProductItems", product_id, "ProductSubImages", variant_id, "video", newFileName)
                    .Replace("\\", "/");

                var image = new im_ProductImages
                {
                    image_id = Guid.CreateVersion7(),
                    product_id = guid_product_id,
                    variant_id = guid_varientid,
                    image_url = relativePath, // Saving video path here
                    uploaded_at = DateTime.Now,
                    is_primary = "F",
                    display_order = itemNumber
                };
                _context.im_ProductImages.Add(image);

            }

            // Update next_key and save
            am_table.next_key = next_key;
            _context.am_table_next_key.Update(am_table);

            await _context.SaveChangesAsync();

            return new ServiceResult<string>
            {
                Success = true,
                Message = "All video files uploaded successfully.",
                Data = null
            };
        }

        public async Task<ServiceResult<string>> get_company_product(string company_id)
        {
            if (company_id == null)
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "not found"
                };
            }
            //var all_product_details = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_PriceTiers)
            //                          .ThenInclude(a => a.im_ProductVariantPrices).ThenInclude(a => a.im_ProductImages).FirstOrDefaultAsync(a => a.company_id == company_id);
            var jsonData = JsonConvert.SerializeObject(all_product_details);
            var encryptedData = EncryptionHelper.EncryptString(jsonData);
            return new ServiceResult<string>
            {
                Success = true,
                Message = "successfully",
                Data = encryptedData
            };
        }

        public async Task<ServiceResult<im_Products>> all_product_details(string company_id)
        {
            if (company_id == null)
            {
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "not found"
                };
            }
            var guid_companyid= Guid.Parse(company_id);
            var all_product_details = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a=>a.im_Product_Subvariants).ThenInclude(a => a.im_PriceTiers)
                                      .ThenInclude(a => a.im_ProductVariantPrices).FirstOrDefaultAsync(a => a.company_id == guid_companyid);
            return new ServiceResult<im_Products>
            {
                Success = true,
                Message = "successfully",
                Data=all_product_details
            };

        }
        public async Task<ActionResult<ServiceResult<im_products_dto>>> Get_product_details(string product_id)
        {
            if (product_id == null)
            {
                return new ServiceResult<im_products_dto>
                {
                    Success = false,
                    Message = "No id found"
                };
            }
            var guid_product = Guid.Parse(product_id);

            var all_product_details = await _context.im_Products
                .Include(a => a.im_ProductVariants)
                    .ThenInclude(a => a.im_Product_Subvariants)
                        .ThenInclude(a => a.im_PriceTiers)
                            .ThenInclude(a => a.im_ProductVariantPrices)
                .FirstOrDefaultAsync(a => a.product_id == guid_product);

            if (all_product_details == null)
            {
                return new ServiceResult<im_products_dto>
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            var productImages = await _context.im_ProductImages
                .Where(img => img.product_id == guid_product)
                .ToListAsync();

            im_products_dto im_Products_Dto = new im_products_dto();
            var im_prodct = await _context.im_Products.FirstOrDefaultAsync(a=>a.product_id== guid_product);
            im_Products_Dto.product_id = all_product_details.product_id;
            im_Products_Dto.updated_at = all_product_details.updated_at;
            im_Products_Dto.im_ProductVariants_dto = new List<im_ProductVariants_dto>();

            foreach (var product in all_product_details.im_ProductVariants)
            {
                var im_prodct_varient = im_prodct.im_ProductVariants.FirstOrDefault(a => a.variant_id == product.variant_id);
                im_ProductVariants_dto im_ProductVariants_Dto = new im_ProductVariants_dto();
                im_ProductVariants_Dto.price = product.price;
                im_ProductVariants_Dto.im_Product_Subvariants_dto = new List<im_Product_Subvariants_dto>();

                foreach (var im_prodct_sub_varient in product.im_Product_Subvariants)
                {
                    var im_prodcut_sub_varient = im_prodct_varient.im_Product_Subvariants.FirstOrDefault(a => a.sub_variant_id == im_prodct_sub_varient.sub_variant_id);
                    im_Product_Subvariants_dto im_ProductSubvariants_ = new im_Product_Subvariants_dto();
                    im_ProductSubvariants_.list_price = im_prodct_sub_varient.list_price;
                    im_ProductSubvariants_.im_PriceTiers_dto = new List<im_PriceTiers_dto>();

                    foreach (var im_price in im_prodct_sub_varient.im_PriceTiers)
                    {
                        var im_prices = im_prodcut_sub_varient.im_PriceTiers.FirstOrDefault(a => a.price_tier_id == im_price.price_tier_id);
                        im_PriceTiers_dto im_PriceTiers_Dto = new im_PriceTiers_dto();
                        im_PriceTiers_Dto.price_tier_id = im_price.price_tier_id;
                        im_PriceTiers_Dto.im_ProductVariantPrices_dto = new List<im_ProductVariantPrices_dto>();

                        foreach (var im_productvarient_price in im_price.im_ProductVariantPrices)
                        {
                            var im_prodctvarient = im_prices.im_ProductVariantPrices.FirstOrDefault(a => a.variant_price_id == im_productvarient_price.variant_price_id);
                            im_ProductVariantPrices_dto im_ProductVariantPrices_Dto1 = new im_ProductVariantPrices_dto();
                            im_ProductVariantPrices_Dto1.price = im_productvarient_price.price;
                            im_ProductVariantPrices_Dto1.im_ProductImages_dto = new List<im_ProductImages_dto>();

                            var images = productImages
                                .Where(a => a.product_id == all_product_details.product_id)
                                .ToList();

                            foreach (var img in images)
                            {
                                im_ProductVariantPrices_Dto1.im_ProductImages_dto.Add(new im_ProductImages_dto
                                {
                                    image_id = img.image_id,
                                    product_id = img.product_id,
                                    variant_id = img.variant_id,
                                    image_url = img.image_url,
                                    is_primary = img.is_primary,
                                    display_order = img.display_order,
                                    uploaded_at = img.uploaded_at
                                });
                            }

                            // Add the variant price DTO to the PriceTier's list
                            im_PriceTiers_Dto.im_ProductVariantPrices_dto.Add(im_ProductVariantPrices_Dto1);
                        }

                        // Add the PriceTier DTO to the Subvariant's list
                        im_ProductSubvariants_.im_PriceTiers_dto.Add(im_PriceTiers_Dto);
                    }

                    // Add the Subvariant DTO to the Variant's list
                    im_ProductVariants_Dto.im_Product_Subvariants_dto.Add(im_ProductSubvariants_);
                }

                // Add the Variant DTO to the Product's list
                im_Products_Dto.im_ProductVariants_dto.Add(im_ProductVariants_Dto);
            }

            return new ServiceResult<im_products_dto>
            {
                Success = true,
                Message = "successfully",
                Data = im_Products_Dto
            };
        }



        public async Task<ActionResult<ServiceResult<im_Products>>> Update_Product(string product_id, im_Products im_products)
        {
            if (product_id == null)
            {
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "Not found"
                };
            }
            var guid_product_id = Guid.Parse(product_id);
            var product = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a=>a.im_Product_Subvariants).ThenInclude(a => a.im_PriceTiers)
                          .ThenInclude(a => a.im_ProductVariantPrices).FirstOrDefaultAsync(a => a.product_id == guid_product_id);

            product.title = im_products.title;
            product.description = im_products.description;
            product.brand = im_products.brand;
            product.updated_at = DateTime.Now;
            product.free_item = im_products.free_item;
            product.stock = im_products.stock;
            product.status = im_products.status;
            foreach (var varient in im_products.im_ProductVariants)
            {
                var existingVariant = product.im_ProductVariants.FirstOrDefault(v => v.variant_id == varient.variant_id);

                existingVariant.barcode = varient.barcode;
                existingVariant.color = varient.color;
                existingVariant.size = varient.size;
                existingVariant.price = varient.price;
                existingVariant.stock_quantity = varient.stock_quantity;
                existingVariant.updated_at = DateTime.Now;

                foreach(var sub_varient in varient.im_Product_Subvariants)
                {
                    var exising_sub = existingVariant.im_Product_Subvariants.FirstOrDefault(a => a.sub_variant_id == sub_varient.sub_variant_id);
                    exising_sub.variantType = sub_varient.variantType;
                    exising_sub.variantValue = sub_varient.variantValue;
                    exising_sub.list_price=sub_varient.list_price;
                    exising_sub.fixed_price=sub_varient.fixed_price;
                    exising_sub.quantity=sub_varient.quantity;
                    exising_sub.created_at = DateTime.Now;
                    foreach (var price_tire in sub_varient.im_PriceTiers)
                    {
                        var existingTier = exising_sub.im_PriceTiers.FirstOrDefault(t => t.price_tier_id == price_tire.price_tier_id);

                        existingTier.name = price_tire.name;
                        existingTier.description = price_tire.description;
                        

                        foreach (var varient_price in price_tire.im_ProductVariantPrices)
                        {
                            var existingPrice = existingTier.im_ProductVariantPrices.FirstOrDefault(p => p.variant_price_id == varient_price.variant_price_id);

                            existingPrice.price = varient_price.price;
                            existingPrice.updated_at = DateTime.Now;
                        }
                    }
                }
                

            }
            await _context.SaveChangesAsync();
            return new ServiceResult<im_Products>
            {
                Success = true,
                Message = "Product updated successfully.",
            };
        }

        public async Task<ActionResult<ServiceResult<im_ProductVariants>>> Add_subCategory(string product_id, im_ProductVariants im_varint)
        {
            if (product_id == null)
            {
                return new ServiceResult<im_ProductVariants>
                {
                    Success = false,
                    Message = "NO  found"
                };
            }
            //var table = "im_ProductVariants";
            //var am_table = await _context.am_table_next_key.FindAsync(table);
            //var next_key = Convert.ToInt16(am_table.next_key);

            //var table2 = "im_Product_Subvariants";
            //var am_table2 = await _context.am_table_next_key.FindAsync(table2);
            //var next_key2 = Convert.ToInt16(am_table2.next_key);

            //var table3 = "im_PriceTiers";
            //var am_table3 = await _context.am_table_next_key.FindAsync(table3);
            //var next_key3 = Convert.ToInt16(am_table3.next_key);

            //var table4 = "im_ProductVariantPrices";
            //var am_table4 = await _context.am_table_next_key.FindAsync(table4);
            //var next_key4 = Convert.ToInt16(am_table4.next_key);

            var guid_product_id = Guid.Parse(product_id);

            var im_prodct = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a=>a.im_Product_Subvariants)
                .ThenInclude(a => a.im_PriceTiers).ThenInclude(a => a.im_ProductVariantPrices).FirstOrDefaultAsync(a => a.product_id == guid_product_id);


            im_varint.variant_id = Guid.CreateVersion7();
            im_varint.product_id = im_prodct.product_id;
            im_varint.uom_id = im_varint.uom_id;
            im_varint.sku = im_varint.sku;
            im_varint.color = im_varint.color;
            im_varint.size = im_varint.size;
            im_varint.price = im_varint.price;
            im_varint.stock_quantity = im_varint.stock_quantity;
            im_varint.weight_kg = im_varint.weight_kg;
            im_varint.width_cm = im_varint.width_cm;
            im_varint.height_cm = im_varint.height_cm;
            im_varint.length_cm = im_varint.length_cm;
            im_varint.chargeable_weight_kg = im_varint.chargeable_weight_kg;
            im_varint.created_at = DateTime.Now;
            im_varint.updated_at = DateTime.Now;
            im_varint.allow_below_Zero = im_varint.allow_below_Zero;
            im_varint.low_stock_alert = im_varint.low_stock_alert;

            List<im_product_subvariant> im_Product_Subvariants = new List<im_product_subvariant>();
            foreach(var sub_varient in im_varint.im_Product_Subvariants)
            {
                sub_varient.sub_variant_id = Guid.CreateVersion7();
                sub_varient.variant_id = im_varint.variant_id;
                sub_varient.product_id = im_varint.product_id;
                sub_varient.variantType = sub_varient.variantType;
                sub_varient.variantValue = sub_varient.variantValue;
                sub_varient.list_price = sub_varient.list_price;
                sub_varient.standard_cost = sub_varient.standard_cost;
                sub_varient.last_cost = sub_varient.last_cost;
                sub_varient.avg_cost = sub_varient.avg_cost;
                sub_varient.ws_price = sub_varient.ws_price;
                sub_varient.profit_p = sub_varient.profit_p;
                sub_varient.minimum_selling = sub_varient.minimum_selling;
                sub_varient.item_barcode = sub_varient.item_barcode;
                sub_varient.modal_number = sub_varient.modal_number;
                sub_varient.shipping_weight = sub_varient.shipping_weight;
                sub_varient.shipping_length = sub_varient.shipping_length;
                sub_varient.shipping_width = sub_varient.shipping_width;
                sub_varient.shipping_height = sub_varient.shipping_height;
                sub_varient.total_volume = sub_varient.total_volume;
                sub_varient.total_weight = sub_varient.total_weight;
                sub_varient.deduct_qnty = sub_varient.deduct_qnty;
                sub_varient.quantity = sub_varient.quantity;
                sub_varient.created_at = DateTime.Now;
                sub_varient.edit_user_id = sub_varient.edit_user_id;
                sub_varient.unit_breakdown = sub_varient.unit_breakdown;
                sub_varient.fixed_price = sub_varient.fixed_price;
                sub_varient.generateBarcode = sub_varient.generateBarcode;


                List<im_PriceTiers> im_PriceTiers = new List<im_PriceTiers>();

                foreach (var price_tair in sub_varient.im_PriceTiers)
                {
                    price_tair.price_tier_id = Guid.CreateVersion7();
                    price_tair.variant_id = im_varint.variant_id;
                    price_tair.name = price_tair.name;
                    price_tair.description = price_tair.description;

                    List<im_ProductVariantPrices> im_ProductVariantPrices = new List<im_ProductVariantPrices>();

                    foreach (var price_varient in price_tair.im_ProductVariantPrices)
                    {
                        price_varient.variant_price_id = Guid.CreateVersion7();
                        price_varient.sub_variant_id = sub_varient.sub_variant_id;
                        price_varient.company_id = im_prodct.company_id;
                        price_varient.price_tier_id = price_tair.price_tier_id;
                        price_varient.price = price_varient.price;
                        price_varient.currency = price_varient.currency;
                        price_varient.created_at = DateTime.Now;
                        price_varient.updated_at = DateTime.Now;

                        im_ProductVariantPrices.Add(price_varient);
                    }
                    price_tair.im_ProductVariantPrices = im_ProductVariantPrices;
                    im_Product_Subvariants.Add(sub_varient);
                }
            }
            

            im_varint.im_Product_Subvariants = im_Product_Subvariants;
            im_prodct.im_ProductVariants.Add(im_varint);



            

            //_context.am_table_next_key.Update(am_table);
            //_context.am_table_next_key.Update(am_table2);
            //_context.am_table_next_key.Update(am_table3);

            await _context.SaveChangesAsync();
            return new ServiceResult<im_ProductVariants>
            {
                Success = true,
                Message = "successfully",
                Data=im_varint
                
            };
        }

        public async Task<ActionResult<ServiceResult<im_Products>>> Delete_product(string product_id)
        {
            if (product_id == null)
            {
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "Not found"
                };
            }
            //var im_prodct = await _context.im_Products.Include(a => a.im_ProductVariants)
            //  .ThenInclude(a => a.im_PriceTiers).ThenInclude(a => a.im_ProductVariantPrices).ThenInclude(a => a.im_ProductImages).FirstOrDefaultAsync(a => a.product_id == product_id);
            //var fullImagePath1 = Path.Combine(_webHostEnvironment.WebRootPath, im_prodct.thumbnail_url.Replace("/", Path.DirectorySeparatorChar.ToString()));
            //if (System.IO.File.Exists(fullImagePath1))
            //{
            //    System.IO.File.Delete(fullImagePath1);
            //}
            //foreach (var variant in im_prodct.im_ProductVariants)
            //{
            //    foreach(var price_tire in variant.im_PriceTiers)
            //    {
            //        foreach(var im_product_varient in price_tire.im_ProductVariantPrices)
            //        {
            //            foreach(var image in im_product_varient.im_ProductImages)
            //            {
            //                var fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, image.image_url.Replace("/", Path.DirectorySeparatorChar.ToString()));
            //                if (System.IO.File.Exists(fullImagePath))
            //                {
            //                    System.IO.File.Delete(fullImagePath);
            //                }

            //                _context.im_ProductImages.Remove(image);
            //            }
            //            _context.im_ProductVariantPrices.Remove(im_product_varient);
            //        }
            //        _context.im_PriceTiers.Remove(price_tire);
            //    }
            //    _context.im_ProductVariants.Remove(variant);
            //}
            //_context.im_Products.Remove(im_prodct);

            await _context.SaveChangesAsync();

            return new ServiceResult<im_Products>
            {
                Success = true,
                Message = "Product and all related data deleted successfully.",
            };
        }
    }
}
