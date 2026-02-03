using Amazon.S3;
using Amazon.S3.Model;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.Product_dto;
using Faahi.Model.im_products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Faahi.Service.im_products
{
    public class im_product : Iim_products
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<im_product> _logger;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configure;

        public im_product(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<im_product> logger, IAmazonS3 s3Client, IConfiguration configure)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _configure = configure;
        }


        public async Task<ServiceResult<im_Products>> Create_Product(im_Products im_Product)
        {
            if (im_Product == null)
            {
                _logger.LogWarning("Create_Product: No data was inserted");
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "No data was inserted"
                };
            }
            try
            {

                Random rnd = new Random();
                var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == im_Product.store_id);
                st_store.store_code = st_store.store_code;

                im_Product.product_id = Guid.CreateVersion7();
                im_Product.company_id = im_Product.company_id;
                im_Product.category_id = im_Product.category_id;
                im_Product.sub_category_id = im_Product.sub_category_id;
                im_Product.sub_sub_category_id = im_Product.sub_sub_category_id;
                im_Product.store_id = im_Product.store_id;
                im_Product.title = im_Product.title;
                im_Product.description = im_Product.description;
                im_Product.brand = im_Product.brand;
                im_Product.vendor_Code = im_Product.vendor_Code;
                im_Product.created_at = DateTime.Now;
                im_Product.updated_at = DateTime.Now;
                im_Product.dutyP = im_Product.dutyP;
                im_Product.featured_item = im_Product.featured_item;
                im_Product.ignore_direct = im_Product.ignore_direct;
                im_Product.ignore_direct = im_Product.ignore_direct;
                im_Product.restrict_HS = im_Product.restrict_HS;
                im_Product.status = im_Product.status;
                im_Product.is_varient = im_Product.is_varient;

                foreach (var im_varint in im_Product.im_ProductVariants)
                {
                    var table = "im_ProductVariants";
                    var am_table = await _context.am_table_next_key.FindAsync(table);
                    var key = Convert.ToInt16(am_table.next_key);


                    im_varint.variant_id = Guid.CreateVersion7();
                    im_varint.product_id = im_Product.product_id;
                    im_varint.uom_name = im_varint.uom_name;
                    im_varint.description_2 = im_varint.description_2;
                    im_varint.im_Product = null;

                    im_varint.sku = st_store.store_code + "-" + Convert.ToString(key + 1);
                    if (am_table != null)
                    {
                        am_table.next_key = key + 1;
                        _context.am_table_next_key.Update(am_table);
                        await _context.SaveChangesAsync();
                    }

                    if (im_varint.barcode == null || im_varint.barcode == "" || im_varint.barcode=="0")
                    {
                        string first3 = Regex.Replace(im_Product.title ?? "", @"\s+", "")
                                         .Substring(0, Math.Min(3, (im_Product.title ?? "").Length))
                                         .ToUpper();
                        string randomNumber = rnd.Next(100000, 999999).ToString();

                        im_varint.barcode = $"{first3}{randomNumber}";

                    }
                    else
                    {
                        im_varint.barcode = im_varint.barcode;
                    }
                    im_varint.created_at = DateTime.Now;
                    im_varint.updated_at = DateTime.Now;
                    foreach (var varient_attrbut in im_varint.im_VariantAttributes)
                    {
                        varient_attrbut.varient_attribute_id = Guid.CreateVersion7();

                        varient_attrbut.value_id = varient_attrbut.value_id;
                        varient_attrbut.attribute_id = varient_attrbut.attribute_id;
                        varient_attrbut.variant_id = im_varint.variant_id;
                    }
                    foreach (var store_inv in im_varint.im_StoreVariantInventory)
                    {
                        store_inv.store_variant_inventory_id = Guid.CreateVersion7();
                        store_inv.variant_id = im_varint.variant_id;
                        store_inv.company_id = store_inv.company_id;
                        store_inv.store_id = store_inv.store_id;
                        store_inv.on_hand_quantity = store_inv.on_hand_quantity;
                        store_inv.committed_quantity = store_inv.committed_quantity;
                        store_inv.bin_number = store_inv.bin_number;
                    }
                }
                _context.im_Products.Add(im_Product);



                await _context.SaveChangesAsync();

                return new ServiceResult<im_Products>
                {
                    Status = 1,
                    Success = true,
                    Message = " created successfully",
                    Data = im_Product
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create_Product: An error occurred while creating the product");
                return new ServiceResult<im_Products>
                {
                    Status = 500,
                    Success = false,
                    Message = "An error occurred while creating the product"
                };
            }

        }

        public async Task<ServiceResult<List<im_ProductVariants>>> Add_varient(List<im_ProductVariants> im_ProductVariants, Guid product_id)
        {
            try
            {
                if (im_ProductVariants == null)
                {
                    _logger.LogInformation("Inserting List<im_ProductVariants> was NUll");
                    return new ServiceResult<List<im_ProductVariants>>
                    {
                        Status = 400,
                        Success = false,
                        Message = "NO data to found to insert"
                    };
                }
                Random rnd = new Random();

                var im_product = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_ProductImages)
                .FirstOrDefaultAsync(a => a.product_id == product_id);
                foreach (var item in im_ProductVariants)
                {
                    item.variant_id = Guid.CreateVersion7();
                    item.product_id = product_id;
                    item.uom_name = item.uom_name;
                    var namePart = Regex.Replace(im_product.title ?? "", @"\s+", "")
                            .Substring(0, Math.Min(3, (im_product.title ?? "").Length))
                            .ToUpper();
                    var SKU = namePart + "-";
                    item.sku = SKU;
                    if (item.barcode == null || item.barcode == "" || item.barcode == "0")
                    {
                        string first3 = Regex.Replace(im_product.title ?? "", @"\s+", "")
                                         .Substring(0, Math.Min(3, (im_product.title ?? "").Length))
                                         .ToUpper();
                        string randomNumber = rnd.Next(100000, 999999).ToString();

                        item.barcode = $"{first3}{randomNumber}";

                    }
                    else
                    {
                        item.barcode = item.barcode;
                    }
                    foreach (var varient_attrbut in item.im_VariantAttributes)
                    {
                        varient_attrbut.varient_attribute_id = Guid.CreateVersion7();

                        varient_attrbut.value_id = varient_attrbut.value_id;
                        varient_attrbut.attribute_id = varient_attrbut.attribute_id;
                        varient_attrbut.variant_id = item.variant_id;
                    }
                    foreach (var store_inv in item.im_StoreVariantInventory)
                    {
                        store_inv.store_variant_inventory_id = Guid.CreateVersion7();
                        store_inv.variant_id = item.variant_id;
                        store_inv.company_id = store_inv.company_id;
                        store_inv.store_id = store_inv.store_id;
                        store_inv.on_hand_quantity = store_inv.on_hand_quantity;
                        store_inv.committed_quantity = store_inv.committed_quantity;
                        store_inv.bin_number = store_inv.bin_number;
                    }
                    await _context.im_ProductVariants.AddAsync(item);
                    im_product.im_ProductVariants.Add(item); 


                }
                _context.im_Products.Update(im_product);
                await _context.SaveChangesAsync();
                return new ServiceResult<List<im_ProductVariants>>
                {
                    Status = 201,
                    Success = true,
                    Data = im_ProductVariants
                };
            }
            catch(Exception  ex)
            {
                return new ServiceResult<List<im_ProductVariants>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                };
            }
            
        }


        public async Task<ActionResult<ServiceResult<string>>> UploadProductAsync(IFormFile formFile, Guid product_id)
        {
            if (formFile == null || formFile.Length == 0)
            {
                _logger.LogWarning("UploadProductAsync: No file uploaded");
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No file uploaded.",
                    Data = null
                };
            }

            try
            {
                var product = await _context.im_Products.FirstOrDefaultAsync(p => p.product_id == product_id);

                if (product == null)
                {
                    _logger.LogWarning("UploadProductAsync: Invalid product ID");
                    return new ServiceResult<string>
                    {
                        Status=-2,
                        Success = false,
                        Message = "Invalid product ID",
                        Data = null
                    };
                }

                var co_business = await _context.co_business.FirstOrDefaultAsync(c => c.company_id == product.company_id);
                long planLimitInBytes;

                if (co_business.plan_type == "Basic")
                {
                    planLimitInBytes = 5L * 1024 * 1024 * 1024; // 5 GB 
                }
                else if (co_business.plan_type == "Intermediate")
                {
                    planLimitInBytes = 20L * 1024 * 1024 * 1024; // 20 GB
                }
                else if (co_business.plan_type == "Advanced")
                {
                    planLimitInBytes = 50L * 1024 * 1024 * 1024; // 50 GB
                }
                else
                {
                    planLimitInBytes = 5L * 1024 * 1024 * 1024; // default 5 GB
                }


                string storeName = co_business.business_name;
                long storeFolderSize = await GetStoreFolderSizeAsync(_configure["Wasabi:BucketName"], storeName);
                if (storeFolderSize + formFile.Length > planLimitInBytes)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Store storage limit reached",
                        Data = null
                    };
                }

                var bucketName = _configure["Wasabi:BucketName"];

                // Optional: delete old image
                if (!string.IsNullOrEmpty(product.thumbnail_url))
                {
                    var oldKey = new Uri(product.thumbnail_url).AbsolutePath.TrimStart('/');
                    await _s3Client.DeleteObjectAsync(bucketName, oldKey);
                }

                // Prepare new file
                // Prepare new file
                FileInfo fileInfo = new FileInfo(formFile.FileName);
                var newFileName = $"Item_{product.product_id}_1{fileInfo.Extension}";

                // NEW CORRECT FOLDER STRUCTURE
                var key = $"faahi/company/{co_business.company_code}/product_{product.product_id}/default/{newFileName}";

                // Upload to Wasabi
                using var stream = formFile.OpenReadStream();
                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = stream,
                    ContentType = formFile.ContentType,
                    CannedACL = S3CannedACL.PublicRead,
                    Headers = { CacheControl = "public,max-age=604800" } // 1 week caching
                };
                await _s3Client.PutObjectAsync(request);

                // Save canonical URL in DB (no cache-busting)
                product.thumbnail_url = $"https://cdn.faahi.com/{key}";
                _context.im_Products.Update(product);
                await _context.SaveChangesAsync();

                // Return cache-busted URL to client
                var cacheBustedUrl = $"{product.thumbnail_url}?v={DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

                return new ServiceResult<string>
                {
                    Status = 1,
                    Success = true,
                    Message = "File uploaded",
                    Data = cacheBustedUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadProductAsync: An error occurred while uploading the file");
                return new ServiceResult<string>
                {
                    Status = 500,
                    Success = false,
                    Message = "An error occurred while uploading the file.",
                    Data = null
                };
            }
        }

        public async Task<long> GetStoreFolderSizeAsync(string bucketName, string storeName)
        {
            long totalSize = 0;

            var request = new Amazon.S3.Model.ListObjectsV2Request
            {
                BucketName = bucketName,
                Prefix = $"faahi/company/{storeName}/"
                // All objects under this store
            };

            ListObjectsV2Response response;
            do
            {
                response = await _s3Client.ListObjectsV2Async(request);

                foreach (var obj in response.S3Objects)
                {
                    totalSize += obj.Size;
                }

                request.ContinuationToken = response.NextContinuationToken;

            } while (response.IsTruncated);

            return totalSize;
        }

        public async Task<ActionResult<ServiceResult<string>>> UploadMutiple_image(IFormFile[] formFile, string product_id, string variant_id)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No files uploaded.",
                    Data = null
                };
            }

            try
            {
                var guidProductId = Guid.Parse(product_id);
                var guidVariantId = Guid.Parse(variant_id);

                var variant = await _context.im_ProductVariants
                    .Include(v => v.im_ProductImages)
                    .FirstOrDefaultAsync(v => v.variant_id == guidVariantId);

                if (variant == null)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Invalid variant ID.",
                        Data = null
                    };
                }

                var product = await _context.im_Products
                    .FirstOrDefaultAsync(p => p.product_id == guidProductId);

                var business = await _context.co_business
                    .FirstOrDefaultAsync(c => c.company_id == product.company_id);

                var bucketName = _configure["Wasabi:BucketName"];


                // -----------------------------------------
                // DELETE OLD IMAGES (WASABI + DB)
                // -----------------------------------------

                var oldImages = _context.im_ProductImages
                    .Where(img => img.variant_id == guidVariantId)
                    .ToList();

                foreach (var img in oldImages)
                {
                    //var oldKey = new Uri(img.image_url).AbsolutePath.TrimStart('/');
                    //await _s3Client.DeleteObjectAsync(bucketName, oldKey);
                    //_context.im_ProductImages.Remove(img);
                    //_context.SaveChanges();
                }


                // -----------------------------------------
                // SAVE NEW IMAGES
                // -----------------------------------------

                int imageNumber = 0;

                foreach (var file in formFile)
                {
                    imageNumber++;

                    FileInfo info = new FileInfo(file.FileName);

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                    string fileName = $"sub_{imageNumber}_{timestamp}{info.Extension}";

                    // FINAL CORRECT PATH (NO VARIANT FOLDER)
                    string key =
                        $"faahi/company/{business.company_code}/product_{product_id}/sub_images/{fileName}";

                    using var stream = file.OpenReadStream();

                    var req = new Amazon.S3.Model.PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        InputStream = stream,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead,
                        Headers = { CacheControl = "public,max-age=604800" }
                    };

                    await _s3Client.PutObjectAsync(req);

                    string canonicalUrl = $"https://cdn.faahi.com/{key}";

                    var imageEntity = new im_ProductImages
                    {
                        image_id = Guid.CreateVersion7(),
                        product_id = guidProductId,
                        variant_id = guidVariantId,
                        image_url = canonicalUrl,
                        uploaded_at = DateTime.Now,
                        is_primary = "F",
                        display_order = imageNumber
                    };

                    _context.im_ProductImages.Add(imageEntity);
                    variant.im_ProductImages.Add(imageEntity);
                }
                 _context.im_ProductVariants.Update(variant);
                await _context.SaveChangesAsync();


                return new ServiceResult<string>
                {
                    Status = 1,
                    Success = true,
                    Message = "Images uploaded successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadMutiple_image error");

                return new ServiceResult<string>
                {
                    Status = 500,
                    Success = false,
                    Message = "Error uploading images.",
                    Data = null
                };
            }
        }

        public async Task<ServiceResult<DeleteImageDto>> Delete_ProductImage(DeleteImageDto deleteImageDto)
        {
            if (deleteImageDto == null || deleteImageDto.Deleted_Images == null || !deleteImageDto.Deleted_Images.Any())
            {
                _logger.LogWarning("Delete_ProductImage: No images provided");
                return new ServiceResult<DeleteImageDto>
                {
                    Success = false,
                    Message = "No images provided for deletion"
                };
            }

            try
            {
                // Get all images for the variant into memory
                var images = await _context.im_ProductImages
                    .Where(a => a.variant_id == deleteImageDto.Variant_Id)
                    .ToListAsync();

                // Filter in memory using Path.GetFileName
                var imagesToDelete = images
                    .Where(img => deleteImageDto.Deleted_Images.Contains(Path.GetFileName(img.image_url)))
                    .ToList();

                // Delete the images
                foreach (var img in imagesToDelete)
                {
                    var bucketName = _configure["Wasabi:BucketName"];
                    var oldKey = new Uri(img.image_url).AbsolutePath.TrimStart('/');
                    await _s3Client.DeleteObjectAsync(bucketName, oldKey);
                    _context.im_ProductImages.Remove(img);
                }

                //await _context.SaveChangesAsync();


                await _context.SaveChangesAsync();

                return new ServiceResult<DeleteImageDto>
                {
                    Success = true,
                    Message = "Selected images deleted successfully",
                    Data = deleteImageDto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete_ProductImage: Error while deleting images");
                return new ServiceResult<DeleteImageDto>
                {
                    Success = false,
                    Message = "An error occurred while deleting the product images"
                };
            }
        }


        public async Task<ActionResult<ServiceResult<string>>> Upload_vedio(IFormFile[] formFile, string product_id, string variant_id)
        {
            if (formFile == null || formFile.Length == 0)
            {
                _logger.LogWarning("Upload_vedio: No video files were uploaded");
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No video files uploaded.",
                    Data = null
                };
            }
            try
            {
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
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "ProductItems", product_id, "video", variant_id);

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


                await _context.SaveChangesAsync();

                return new ServiceResult<string>
                {
                    Status = 1,
                    Success = true,
                    Message = "All video files uploaded successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Upload_vedio: An error occurred while uploading video files");
                return new ServiceResult<string>
                {
                    Status = 500,
                    Success = false,
                    Message = "An error occurred while uploading video files.",
                    Data = null
                };
            }


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
            var jsonResult = (await _context.Database.SqlQueryRaw<string>(
                    "EXEC dbo.GetAllProductDetails_JSON @CompanyId = @CompanyId, @opr = @opr",
                     new SqlParameter("@CompanyId", company_id),
                    new SqlParameter("@opr", 3)).ToListAsync()).FirstOrDefault();
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

        //public async Task<ServiceResult<List<im_Products>>> all_product_details(Guid company_id)
        //{
        //    if (company_id == null)
        //    {
        //        return new ServiceResult<List<im_Products>>
        //        {
        //            Success = false,
        //            Message = "not found"
        //        };
        //    }
        //    var im_prodcut = await _context.im_Products.FirstOrDefaultAsync(a => a.product_id == company_id);
        //    var all_product_details = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_ProductImages)
        //                              .Where(a => a.company_id == company_id).ToListAsync();
        //    return new ServiceResult<List<im_Products>>
        //    {
        //        Success = true,
        //        Message = "successfully",
        //        Data = all_product_details
        //    };

        //}
        public async Task<ServiceResult<List<im_Products>>> all_product_details(Guid company_id)
        {
            if (company_id == Guid.Empty)
            {
                return new ServiceResult<List<im_Products>>
                {
                    Success = false,
                    Message = "Company ID not found"
                };
            }

            var jsonResult = _context.Database
               .SqlQueryRaw<string>(
                   "EXEC dbo.GetAllProductDetails_JSON @CompanyId = @CompanyId, @opr = @opr_param",
                   new SqlParameter("@CompanyId", company_id),
                   new SqlParameter("@opr_param", 1) 
               )
               .AsEnumerable()
               .FirstOrDefault();
            


            if (string.IsNullOrEmpty(jsonResult))
            {
                return new ServiceResult<List<im_Products>>
                {
                    Success = false,
                    Message = "No products found",
                    Data = new List<im_Products>()
                };
            }

            var products = JsonConvert.DeserializeObject<List<im_Products>>(jsonResult);

            return new ServiceResult<List<im_Products>>
            {
                Success = true,
                Message = "Successfully retrieved products",
                Data = products
            };
        }
        //public async Task<ServiceResult<im_Products>> Get_product_details(Guid product_id)
        //{
        //    if (product_id == null)
        //    {
        //        return new ServiceResult<im_Products>
        //        {
        //            Success = false,
        //            Message = "No id found"
        //        };
        //    }

        //    var all_product_details = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_ProductImages)
        //        .FirstOrDefaultAsync(a => a.product_id == product_id);

        //    if (all_product_details == null)
        //    {
        //        return new ServiceResult<im_Products>
        //        {
        //            Success = false,
        //            Message = "Product not found"
        //        };
        //    }



        //    return new ServiceResult<im_Products>
        //    {
        //        Success = true,
        //        Message = "successfully",
        //        Data = all_product_details
        //    };
        //}
        public async Task<ServiceResult<im_Products>> Get_product_details(Guid product_id)
        {
            if (product_id == null)
            {
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "No id found"
                };
            }

            var jsonResult = _context.Database
                .SqlQueryRaw<string>(
                    "EXEC dbo.GetAllProductDetails_JSON @product_id = @product_id_param, @opr = @opr_param",
                    new SqlParameter("@product_id_param", product_id),
                    new SqlParameter("@opr_param", 2) // single product
                )
                .AsEnumerable()
                .FirstOrDefault();


            if (all_product_details == null)
            {
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "Product not found"
                };
            }
                var products = JsonConvert.DeserializeObject<im_Products>(jsonResult);



            return new ServiceResult<im_Products>
            {
                Success = true,
                Message = "successfully",
                Data = products
            };
        }



        public async Task<ActionResult<ServiceResult<im_Products>>> Update_Product(Guid product_id, im_Products im_products)
        {
            if (product_id == null)
            {
                _logger.LogWarning("Update_Product: No product ID provided");
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "Not found"
                };
            }
            try
            {
                

                var product = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_ProductImages)
                .FirstOrDefaultAsync(a => a.product_id == product_id);
                //var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == product.store_id);
                //st_store.store_code = st_store.store_code;
                product.title = im_products.title;
                product.description = im_products.description;
                product.brand = im_products.brand;
                product.updated_at = DateTime.Now;
                product.stock_flag = im_products.stock_flag;
                product.ignore_direct = im_products.ignore_direct;
                product.low_stock_alert = im_products.low_stock_alert;
                product.restrict_deciaml_qty = im_products.restrict_deciaml_qty;
                product.allow_below_zero = im_products.allow_below_zero;
                product.fixed_price = im_products.fixed_price;
                product.published = im_products.published;
                product.is_varient = im_products.is_varient;
                product.track_expiry = im_products.track_expiry;
                product.status = im_products.status;
                foreach (var varient in im_products.im_ProductVariants)
                {
                    //var table = "im_ProductVariants";
                    //var am_table = await _context.am_table_next_key.FindAsync(table);
                    //var key = Convert.ToInt16(am_table.next_key);
                    varient.im_Product = null;


                    var existingVariant = product.im_ProductVariants.FirstOrDefault(v => v.variant_id == varient.variant_id);
                    string description_2 = varient.description_2;
                    if (existingVariant != null)
                    {
                        existingVariant.description_2 = "";
                        _context.Update(existingVariant);
                    }
                    //existingVariant.sku =st_store.store_code+ "-"+ Convert.ToString(key + 1);
                    //if (am_table != null)
                    //{
                    //    am_table.next_key = key + 1;
                    //    _context.am_table_next_key.Update(am_table);
                    //    await _context.SaveChangesAsync();
                    //}
                   

                    existingVariant.base_price= varient.base_price;
                    existingVariant.description_2 = description_2;
                    existingVariant.uom_name = varient.uom_name;
                    //existingVariant.barcode = varient.barcode;
                    //existingVariant.uom_id = varient.uom_id;
                    //existingVariant.updated_at = DateTime.Now;


                    foreach (var im_store in varient.im_StoreVariantInventory)
                    {
                        var existingStoreInv = existingVariant.im_StoreVariantInventory.FirstOrDefault(s => s.store_variant_inventory_id == im_store.store_variant_inventory_id);
                       
                        existingStoreInv.on_hand_quantity = im_store.on_hand_quantity;
                        existingStoreInv.committed_quantity = im_store.committed_quantity;
                        //existingStoreInv.bin_number = im_store.bin_number;
                    }


                }
                await _context.SaveChangesAsync();
                return new ServiceResult<im_Products>
                {
                    Status=201,
                    Success = true,
                    Message = "Product updated successfully.",
                    Data = product
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update_Product: An error occurred while updating the product");
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "An error occurred while updating the product"
                };
            }

        }
        public async Task<ServiceResult<im_Products>> Update_Mutiple_Product(Guid product_id, im_Products im_Products)
        {
            if (product_id == null)
            {
                _logger.LogWarning("Update_Mutiple_Product: No product ID provided");
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "Not found"
                };
            }
            try
            {
                Random rnd = new Random();

                var product = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_ProductImages)
                                .FirstOrDefaultAsync(a => a.product_id == product_id);
                //var st_store = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == product.store_id);
                //st_store.store_code = st_store.store_code;
                product.title = im_Products.title;
                product.category_id = im_Products.category_id;
                product.sub_category_id = im_Products.sub_category_id;
                product.sub_sub_category_id = im_Products.sub_sub_category_id;
                product.description = im_Products.description;
                product.brand = im_Products.brand;
                product.updated_at = DateTime.Now;
                product.stock_flag = im_Products.stock_flag;
                product.ignore_direct = im_Products.ignore_direct;
                product.low_stock_alert = im_Products.low_stock_alert;
                product.restrict_deciaml_qty = im_Products.restrict_deciaml_qty;
                product.allow_below_zero = im_Products.allow_below_zero;
                product.fixed_price = im_Products.fixed_price;
                product.published = im_Products.published;
                product.status = im_Products.status;
                foreach (var varient in im_Products.im_ProductVariants)
                {
                    //var table = "im_ProductVariants";
                    //var am_table = await _context.am_table_next_key.FindAsync(table);
                    //var key = Convert.ToInt16(am_table.next_key);
                    varient.im_Product = null;

                    var existingVariant = product.im_ProductVariants.FirstOrDefault(v => v.variant_id == varient.variant_id);
                    if(existingVariant != null)
                    {
                        string description_2 = varient.description_2;
                        if (existingVariant != null)
                        {
                            existingVariant.description_2 = "";
                            _context.Update(existingVariant);
                        }
                        //existingVariant.sku = st_store.store_code + "-" + Convert.ToString(key + 1);
                        //if (am_table != null)
                        //{
                        //    am_table.next_key = key + 1;
                        //    _context.am_table_next_key.Update(am_table);
                        //    await _context.SaveChangesAsync();
                        //}
                        existingVariant.base_price = varient?.base_price;
                        existingVariant.barcode = varient.barcode;
                        existingVariant.description_2 = description_2;
                        existingVariant.uom_name = varient.uom_name;

                        foreach (var im_attr in varient.im_VariantAttributes)
                        {
                            var existingAttr = existingVariant.im_VariantAttributes.FirstOrDefault(a => a.varient_attribute_id == im_attr.varient_attribute_id);
                            if (existingAttr != null)
                            {
                                existingAttr.attribute_id = im_attr.attribute_id;
                                existingAttr.value_id = im_attr.value_id;
                            }
                            else
                            {
                                im_attr.varient_attribute_id = Guid.CreateVersion7();
                                im_attr.value_id = im_attr.value_id;
                                im_attr.variant_id = existingVariant.variant_id;
                                im_attr.attribute_id = im_attr.attribute_id;
                                _context.im_VariantAttributes.Add(im_attr);
                                existingVariant.im_VariantAttributes.Add(im_attr);
                            }

                        }

                        foreach (var im_store in varient.im_StoreVariantInventory)
                        {
                            var existingStoreInv = existingVariant.im_StoreVariantInventory.FirstOrDefault(s => s.store_variant_inventory_id == im_store.store_variant_inventory_id);

                            existingStoreInv.on_hand_quantity = im_store.on_hand_quantity;
                            existingStoreInv.committed_quantity = im_store.committed_quantity;

                        }
                    }
                    else
                    {
                        varient.variant_id = Guid.CreateVersion7();
                        varient.product_id = product.product_id;
                        varient.uom_name = varient.uom_name;

                        var namePart = Regex.Replace(product.title ?? "", @"\s+", "")
                      .Substring(0, Math.Min(3, (product.title ?? "").Length))
                      .ToUpper();
                        var SKU = namePart + "-";
                        varient.sku = SKU + rnd.Next(100000, 999999).ToString();
                        varient.sku = varient.sku;

                        varient.created_at = DateTime.Now;
                        varient.updated_at = DateTime.Now;

                        foreach(var im_store in varient.im_StoreVariantInventory)
                        {
                            im_store.store_variant_inventory_id = Guid.CreateVersion7();
                            im_store.variant_id = varient.variant_id;
                            im_store.company_id = im_store.company_id;
                            im_store.store_id = im_store.store_id;
                            im_store.on_hand_quantity = im_store.on_hand_quantity;
                            im_store.committed_quantity = im_store.committed_quantity;
                            im_store.bin_number = im_store.bin_number;
                        }
                        foreach(var im_attr in varient.im_VariantAttributes)
                        {
                            im_attr.varient_attribute_id =  Guid.CreateVersion7();
                            im_attr.value_id = im_attr.value_id;
                            im_attr.variant_id = varient.variant_id;
                            im_attr.attribute_id = im_attr.attribute_id;
                        }

                        _context.im_ProductVariants.Add(varient);
                        product.im_ProductVariants.Add(varient);
                    }
                }
                await _context.SaveChangesAsync();
                return new ServiceResult<im_Products>
                {
                    Status = 201,
                    Success = true,
                    Message = "Product updated successfully.",
                    Data = product
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Update_Mutiple_Product: An error occurred while updating the product");
                return new ServiceResult<im_Products>
                {
                    Success = false,
                    Message = "An error occurred while updating the product"
                };
            }
        }

        public async Task<ActionResult<ServiceResult<im_ProductVariants>>> Add_subCategory(string product_id, im_ProductVariants im_varint)
        {
            if (product_id == null)
            {
                _logger.LogWarning("Add_subCategory: No product ID provided");
                return new ServiceResult<im_ProductVariants>
                {
                    Success = false,
                    Message = "Not  found"
                };
            }
            try
            {
                var guid_product_id = Guid.Parse(product_id);

                var im_prodct = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes)
                    .Include(a => a.im_ProductVariants).FirstOrDefaultAsync(a => a.product_id == guid_product_id);


                //im_varint.variant_id = Guid.CreateVersion7();
                //im_varint.product_id = im_prodct.product_id;
                //im_varint.uom_id = im_varint.uom_id;
                //im_varint.sku = im_varint.sku;
                //im_varint.color = im_varint.color;
                //im_varint.size = im_varint.size;
                //im_varint.price = im_varint.price;
                //im_varint.stock_quantity = im_varint.stock_quantity;
                //im_varint.weight_kg = im_varint.weight_kg;
                //im_varint.width_cm = im_varint.width_cm;
                //im_varint.height_cm = im_varint.height_cm;
                //im_varint.length_cm = im_varint.length_cm;
                //im_varint.chargeable_weight_kg = im_varint.chargeable_weight_kg;
                //im_varint.created_at = DateTime.Now;
                //im_varint.updated_at = DateTime.Now;
                //im_varint.allow_below_Zero = im_varint.allow_below_Zero;
                //im_varint.low_stock_alert = im_varint.low_stock_alert;

                //List<im_product_subvariant> im_Product_Subvariants = new List<im_product_subvariant>();
                //foreach (var sub_varient in im_varint.im_Product_Subvariants)
                //{
                //    sub_varient.sub_variant_id = Guid.CreateVersion7();
                //    sub_varient.variant_id = im_varint.variant_id;
                //    sub_varient.product_id = im_varint.product_id;
                //    sub_varient.variantType = sub_varient.variantType;
                //    sub_varient.variantValue = sub_varient.variantValue;
                //    sub_varient.list_price = sub_varient.list_price;
                //    sub_varient.standard_cost = sub_varient.standard_cost;
                //    sub_varient.last_cost = sub_varient.last_cost;
                //    sub_varient.avg_cost = sub_varient.avg_cost;
                //    sub_varient.ws_price = sub_varient.ws_price;
                //    sub_varient.profit_p = sub_varient.profit_p;
                //    sub_varient.minimum_selling = sub_varient.minimum_selling;
                //    sub_varient.item_barcode = sub_varient.item_barcode;
                //    sub_varient.modal_number = sub_varient.modal_number;
                //    sub_varient.shipping_weight = sub_varient.shipping_weight;
                //    sub_varient.shipping_length = sub_varient.shipping_length;
                //    sub_varient.shipping_width = sub_varient.shipping_width;
                //    sub_varient.shipping_height = sub_varient.shipping_height;
                //    sub_varient.total_volume = sub_varient.total_volume;
                //    sub_varient.total_weight = sub_varient.total_weight;
                //    sub_varient.deduct_qnty = sub_varient.deduct_qnty;
                //    sub_varient.quantity = sub_varient.quantity;
                //    sub_varient.created_at = DateTime.Now;
                //    sub_varient.edit_user_id = sub_varient.edit_user_id;
                //    sub_varient.unit_breakdown = sub_varient.unit_breakdown;
                //    sub_varient.fixed_price = sub_varient.fixed_price;
                //    sub_varient.generateBarcode = sub_varient.generateBarcode;


                //    List<im_PriceTiers> im_PriceTiers = new List<im_PriceTiers>();

                //    foreach (var price_tair in sub_varient.im_PriceTiers)
                //    {
                //        price_tair.price_tier_id = Guid.CreateVersion7();
                //        price_tair.variant_id = im_varint.variant_id;
                //        price_tair.name = price_tair.name;
                //        price_tair.description = price_tair.description;

                //        List<im_ProductVariantPrices> im_ProductVariantPrices = new List<im_ProductVariantPrices>();

                //        foreach (var price_varient in price_tair.im_ProductVariantPrices)
                //        {
                //            price_varient.variant_price_id = Guid.CreateVersion7();
                //            price_varient.sub_variant_id = sub_varient.sub_variant_id;
                //            price_varient.company_id = im_prodct.company_id;
                //            price_varient.price_tier_id = price_tair.price_tier_id;
                //            price_varient.price = price_varient.price;
                //            price_varient.currency = price_varient.currency;
                //            price_varient.created_at = DateTime.Now;
                //            price_varient.updated_at = DateTime.Now;

                //            im_ProductVariantPrices.Add(price_varient);
                //        }
                //        price_tair.im_ProductVariantPrices = im_ProductVariantPrices;
                //        im_PriceTiers.Add(price_tair);
                //    }
                //    sub_varient.im_PriceTiers = im_PriceTiers;
                //    im_Product_Subvariants.Add(sub_varient);
                //}


                //im_varint.im_Product_Subvariants = im_Product_Subvariants;

                im_prodct.im_ProductVariants.Add(im_varint);
                _context.im_ProductVariants.Add(im_varint);


                await _context.SaveChangesAsync();
                return new ServiceResult<im_ProductVariants>
                {
                    Success = true,
                    Message = "successfully",
                    Data = im_varint

                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add_subCategory: An error occurred while adding subcategory");
                return new ServiceResult<im_ProductVariants>
                {
                    Success = false,
                    Message = "An error occurred while adding subcategory"
                };
            }


        }

        public async Task<ServiceResult<List<im_Products>>> Get_product_list()
        {
            try
            {
                var jsonResult =(await _context.Database.SqlQueryRaw<string>(
                    "EXEC dbo.GetAllProductDetails_JSON @opr = @opr",
                    new SqlParameter("@opr", 3)).ToListAsync()).FirstOrDefault();

                var all_product_details =
                    System.Text.Json.JsonSerializer.Deserialize<List<im_Products>>(jsonResult);

                //var all_product_details = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_ProductImages)
                //                      .ToListAsync();
                if (all_product_details == null)
                {
                    return new ServiceResult<List<im_Products>>
                    {
                        Success = false,
                        Message = "Product not found"
                    };
                }
                return new ServiceResult<List<im_Products>>
                {
                    Status=200,
                    Success = true,
                    Message = "successfully",
                    Data = all_product_details
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Get_product_list: An error occurred while fetching product list");
                return new ServiceResult<List<im_Products>>
                {
                    Success = false,
                    Message = "An error occurred while fetching product list"
                };
            }
            
        }
        public async Task<ServiceResult<List<im_Products>>> Product_search(string search_text, Guid store_id)
        {
            if (search_text == null)
            {
                return new ServiceResult<List<im_Products>>
                {
                    Success = false,
                    Message = "not found"
                };
            }
            var jsonResult = (await _context.Database
               .SqlQueryRaw<string>(
                   "EXEC dbo.sp_SearchProducts @store_id = @store_id, @search_text = @search_text",
                   new SqlParameter("@store_id", store_id),
                   new SqlParameter("@search_text", search_text) // single product
               )
               .ToListAsync()).FirstOrDefault();
            if (jsonResult == null)
            {
                return new ServiceResult<List<im_Products>>
                {
                    Success = false
                };

            }
            var products = JsonConvert.DeserializeObject<List<im_Products>>(jsonResult);



            return new ServiceResult<List<im_Products>>
            {
                Success = true,
                Message = "successfully",
                Data = products
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

        public async Task<ServiceResult<im_ProductAttributes>> Create_Attribute(im_ProductAttributes im_ProductAttributes)
        {
            if (im_ProductAttributes == null)
            {
                _logger.LogWarning("NO data found to inset");

                return new ServiceResult<im_ProductAttributes>
                {
                    Status = -1,
                    Success = false,
                    Message = "NO data found to inset"
                };
            }
            try
            {
                var attribute = await _context.im_ProductAttributes.Include(a => a.im_AttributeValues).FirstOrDefaultAsync(a => a.name == im_ProductAttributes.name);
                if (attribute != null)
                {
                    foreach (var attributes in im_ProductAttributes.im_AttributeValues)
                    {
                        var existingAttribute = await _context.im_AttributeValues.FirstOrDefaultAsync(a => a.value.Trim().ToLower() == attributes.value.Trim().ToLower());
                        if (existingAttribute == null)
                        {
                            attributes.value_id = Guid.CreateVersion7();
                            attributes.attribute_id = attribute.attribute_id;
                            attributes.value = attributes.value;
                            attributes.display_order = im_ProductAttributes.display_order;
                            attributes.color_name = attributes.color_name;
                            _context.im_AttributeValues.Add(attributes);
                            attribute.im_AttributeValues.Add(attributes);
                        }
                    }
                    _context.im_ProductAttributes.Update(attribute);

                }
                else
                {

                    im_ProductAttributes.attribute_id = Guid.CreateVersion7();
                    im_ProductAttributes.company_id = im_ProductAttributes.company_id;
                    im_ProductAttributes.name = im_ProductAttributes.name;
                    im_ProductAttributes.display_order = im_ProductAttributes.display_order;
                    foreach (var attribute_val in im_ProductAttributes.im_AttributeValues)
                    {

                        attribute_val.value_id = Guid.CreateVersion7();
                        attribute_val.attribute_id = im_ProductAttributes.attribute_id;
                        attribute_val.value = attribute_val.value;
                        attribute_val.display_order = im_ProductAttributes.display_order;
                        attribute_val.color_name = attribute_val.color_name;
                        _context.im_AttributeValues.Add(attribute_val);
                    }
                    _context.im_ProductAttributes.Add(im_ProductAttributes);
                }

                await _context.SaveChangesAsync();

                return new ServiceResult<im_ProductAttributes>
                {
                    Status = 1,
                    Success = true,
                    Message = "successfully",
                    Data = im_ProductAttributes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add_subCategory: An error occurred while adding subcategory");
                return new ServiceResult<im_ProductAttributes>
                {
                    Success = false,
                    Message = "An error occurred while adding subcategory"
                };
            }
        }
        public async Task<ServiceResult<List<im_ProductAttributes>>> Get_attribute(Guid company_id)
        {
            if (company_id == null)
            {
                _logger.LogWarning("NO data found in the company_id");
                return new ServiceResult<List<im_ProductAttributes>>
                {
                    Status = -1,
                    Success = false,
                    Message = "NO data found in the company_id"
                };
            }
             
            //var attribute = await _context.im_ProductAttributes.OrderByDescending(a => a.attribute_id).Include(a => a.im_AttributeValues.OrderByDescending(a => a.attribute_id)).Where(a => a.company_id == company_id).ToListAsync();
            var jsonResult = _context.Database
        .SqlQueryRaw<string>(
                    "EXEC dbo.GetAllProductAttributes_JSON @CompanyId = @CompanyId",
                    new SqlParameter("@CompanyId", company_id)
                    
                )
        .AsEnumerable()        
        .FirstOrDefault();
            var attributes = JsonConvert.DeserializeObject<List<im_ProductAttributes>>(jsonResult);

            return new ServiceResult<List<im_ProductAttributes>>
            {
                Status = 1,
                Success = true,
                Message = "Success",
                Data = attributes
            };
        }

        public async Task<ServiceResult<im_Products>> barcode_exist(string barcode,Guid store_id)
        {
            try
            {
                if (barcode == null)
                {
                    _logger.LogInformation("No data found in barcode");
                    return new ServiceResult<im_Products>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found in barcode"
                    };
                }
                var existing_barcode = await _context.im_Products.Include(a => a.im_ProductVariants).FirstOrDefaultAsync(a=>a.im_ProductVariants.Any(v=>v.barcode==barcode));
                if (existing_barcode == null)
                {
                    _logger.LogInformation("No infromation found");
                    return new ServiceResult<im_Products>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No infromation found"
                    };
                }
                if (existing_barcode != null)
                {
                    var im_prodct_varient = await _context.im_ProductVariants.FirstOrDefaultAsync(a => a.barcode == barcode);
                    var im_store = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.variant_id == im_prodct_varient.variant_id && a.store_id == store_id);
                    if (im_store!=null)
                    {
                        return new ServiceResult<im_Products>
                        {
                            Status = 300,
                            Success = false,
                            Message = "Already inseted product"
                        };
                    }

                }
                return new ServiceResult<im_Products>
                {
                    Status = 200,
                    Message = "",
                    Success = true,
                    Data = existing_barcode

                };
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error while barcode_exist");
                return new ServiceResult<im_Products>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false,
                };
            }
            
            
        }

        public async Task<ServiceResult<im_product>> product_transfer_store(Guid product_id,Guid store_id)
        {
            try
            {
                if (product_id == null)
                {
                    return new ServiceResult<im_product>
                    {
                        Message = "NO product_id found",
                        Success = false,
                        Status = 400,
                    };
                }
                var existing_prodct = await _context.im_Products.Include(a => a.im_ProductVariants).ThenInclude(a => a.im_VariantAttributes).
                    Include(a => a.im_ProductVariants).ThenInclude(a => a.im_ProductImages).Include(a => a.im_ProductVariants).ThenInclude(a => a.im_StoreVariantInventory).
                    FirstOrDefaultAsync(a => a.product_id == product_id);
                foreach (var im_varint in existing_prodct.im_ProductVariants)
                {
                    foreach (var store_inv in im_varint.im_StoreVariantInventory)
                    {
                        var existing_store = await _context.im_StoreVariantInventory.FirstOrDefaultAsync(a => a.store_variant_inventory_id == store_inv.store_variant_inventory_id);
                        if (im_varint.variant_id==existing_store.variant_id && existing_prodct.company_id == existing_store.company_id &&existing_store.store_id==store_id)
                        {
                            return new ServiceResult<im_product>
                            {
                                Status = 300,
                                Message = "The prodct is already exist",
                                Success = false

                            };
                        }
                        store_inv.store_variant_inventory_id = Guid.CreateVersion7();
                        store_inv.variant_id = im_varint.variant_id;
                        store_inv.company_id = existing_store.company_id;
                        store_inv.store_id = store_id;
                        store_inv.on_hand_quantity = store_inv.on_hand_quantity;
                        store_inv.committed_quantity = store_inv.committed_quantity;
                        store_inv.bin_number = existing_store.bin_number;
                        _context.im_StoreVariantInventory.Add(store_inv);
                        im_varint.im_StoreVariantInventory.Add(store_inv);
                    }
                }
                _context.im_Products.Update(existing_prodct);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_product>
                {
                    Status = 200,
                    Success = true,
                    Message = "Updated"
                };
            }
            catch(Exception ex)
            {
                return new ServiceResult<im_product>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,

                };
            }
            

        }


    }
}
