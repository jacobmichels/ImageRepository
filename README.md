# ImageRepository
Hello! Welcome to the image repository I built for my Shopify Summer 2021 application.

# Please use Firefox
I have tested this application with multiple web browsers. I have founded that Chromium based browsers don't interact well with Identity due to their use of samesite cookies, so I recommend using Firefox to view the site. 

# Usage Instructions
Please note this release targeted 64-bit Windows. It probably won't run on anything else.
1. Go to releases and download the zip files under the 1.0 release.
2. Once downloaded, extract this file somewhere onto your computer.
3. Run the file "ImageRepository.Server.exe". You will need to bypass SmartScreen by clicking learn more then run anyway. This will start the application.
4. Navigate to http://localhost:5000 to access the Image Repository client. It may redirect to HTTPS. Remember to use Firefox please!

From here, you are free to test out the functionality of my image repository! I have created an example account with some images already uploaded for you, but you can create your own account and upload your own images too.
Example account email: example@imagerepo.ca
Example account pass: Abc!12

# 5 things to try out
1. Upload some images. Navigate to the upload page, click browser and select up to 10 images from your computer. Once they are loaded into your client, you can do things like giving them a title/caption before uploading them to the server.
2. Try creating an album. If you have images uploaded to your account, you can navigate to the albums page to create an album. You can give an album a name and add images to them. They are a simple method of organization.
3. Check out public images on the Explore page. If another user has uploaded an image onto their account and unchecked the private checkbox, you will be able to see the image along with it's title and caption here.
4. Try bulk deleting. So you don't need to go through and delete each image one at a time, try out the bulk delete on the delete page. On this page, you can select images by clicking on them and deleting all that are selected. You can also remove all images from your account.
5. Sign up with an external authentication provider. You can use your Google or Microsoft account to register for an account on the site. Just hit register and then choose Google or Microsoft. 