// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Service.Sample.Trippin.Submit
{
    // A custom serializer provider to inject the AnnotatingEntitySerializer.
    public class CustomODataSerializerProvider : DefaultODataSerializerProvider
    {
        private AnnotatingEntitySerializer _annotatingEntitySerializer;

        public CustomODataSerializerProvider(IServiceProvider rootContainer)
            : base(rootContainer)
        {
            _annotatingEntitySerializer = new AnnotatingEntitySerializer(this);
        }

        public override ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
        {
            if (edmType.IsEntity() || edmType.IsComplex())
            {
                return _annotatingEntitySerializer;
            }

            return base.GetEdmTypeSerializer(edmType);
        }
    }


    // A custom entity serializer that adds the score annotation to document entries.
    public class AnnotatingEntitySerializer : ODataResourceSerializer
    {
        public AnnotatingEntitySerializer(ODataSerializerProvider serializerProvider)
            : base(serializerProvider)
        {
        }

        public override ODataProperty CreateStructuralProperty(IEdmStructuralProperty structuralProperty, ResourceContext resourceContext)
        {
            ODataProperty property = base.CreateStructuralProperty(structuralProperty, resourceContext);

            var parent = resourceContext.SerializerContext.ExpandedResource;
            if (parent != null)
            {
                var parentInstance = parent.ResourceInstance;
            }

            return property;
        }


        public override ODataResource CreateResource(SelectExpandNode selectExpandNode, ResourceContext resourceContext)
        {
            ODataResource entry = base.CreateResource(selectExpandNode, resourceContext);

            var parent = resourceContext.SerializerContext.ExpandedResource;
            if (parent != null)
            {
                var parentInstance = parent;
            }

            return entry;
        }
    }
}
