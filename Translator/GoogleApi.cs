using Google.Api.Gax.ResourceNames;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translate.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public class GoogleApi
    {
        string projectId;
        string locaionId;
        TranslationServiceClient client;

        public GoogleApi(string projectId, string locaionId, string jsonCredential)
        {
            this.projectId = projectId;
            this.locaionId = locaionId;

            var builder = new TranslationServiceClientBuilder()
            {
                JsonCredentials = jsonCredential,
            };
            client = builder.Build();
        }


        public string CreateGlossary(string glossaryId, string inputUri)
        {
            try
            {
                var request = new CreateGlossaryRequest
                {
                    ParentAsLocationName = new LocationName(projectId, locaionId),
                    Parent = new LocationName(projectId, locaionId).ToString(),
                    Glossary = new Glossary
                    {
                        Name = new GlossaryName(projectId, locaionId, glossaryId).ToString(),
                        LanguageCodesSet = new Glossary.Types.LanguageCodesSet
                        {
                            LanguageCodes = { "en", "ko", },
                        },
                        InputConfig = new GlossaryInputConfig
                        {
                            GcsSource = new GcsSource { InputUri = inputUri, },
                        },
                    },
                };

                Glossary response = client.CreateGlossary(request).PollUntilCompleted().Result;
                return $"name: {response.Name}, Entry count: {response.EntryCount}, Input URI: {response.InputConfig.GcsSource.InputUri}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public List<string> ListGlossaries()
        {
            ListGlossariesRequest request = new ListGlossariesRequest
            {
                ParentAsLocationName = new LocationName(projectId, locaionId),
            };
            var response = client.ListGlossaries(request);
            // Iterate over glossaries and display each glossary's details.
            
            return response.Select(x =>
                $"name: {x.Name}, Entry count: {x.EntryCount}, Input URI: {x.InputConfig.GcsSource.InputUri}").ToList();
        }

        public bool DeleteGlossary(string glossaryId)
        {
            try
            {
                var request = new DeleteGlossaryRequest
                {
                    GlossaryName = new GlossaryName(projectId, locaionId, glossaryId),
                };

                DeleteGlossaryResponse response = client.DeleteGlossary(request).PollUntilCompleted().Result;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


        public async ValueTask<string> TranslateText(string glossaryId, string text)
        {
            var glossaryConfig = new TranslateTextGlossaryConfig()
            {
                Glossary = $"projects/{projectId}/locations/{locaionId}/glossaries/{glossaryId}"
            };

            var request = new TranslateTextRequest
            {
                Contents = { text, },
                SourceLanguageCode = "en",
                TargetLanguageCode = "ko",
                MimeType = "text/html",
                ParentAsLocationName = new LocationName(projectId, locaionId),
                GlossaryConfig = glossaryConfig
            };

            var response = await client.TranslateTextAsync(request);

            return response.GlossaryTranslations.FirstOrDefault()?.TranslatedText;
        }

        public bool BatchTranslateText(string sourceUri, string destUri, string glossaryId)
        {
            try
            {
                var request = new BatchTranslateTextRequest()
                {
                    ParentAsLocationName = new LocationName(projectId, locaionId),
                    SourceLanguageCode = "en",
                    TargetLanguageCodes = { "ko" },
                    InputConfigs = 
                    { 
                        new InputConfig() { MimeType = "text/html", GcsSource = new() { InputUri = sourceUri } } 
                    },
                    OutputConfig = new OutputConfig { GcsDestination = new() { OutputUriPrefix = destUri } },
                    Glossaries = 
                    { 
                        { "ko", new() { Glossary = $"projects/{projectId}/locations/{locaionId}/glossaries/{glossaryId}" } } 
                    },
                };

                var response = client.BatchTranslateText(request).PollUntilCompleted().Result;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
