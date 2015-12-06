// 
// Copyright (c) 2004-2011 Jaroslaw Kowalski <jaak@jkowalski.net>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer. 
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution. 
// 
// * Neither the name of Jaroslaw Kowalski nor the names of its 
//   contributors may be used to endorse or promote products derived from this
//   software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF 
// THE POSSIBILITY OF SUCH DAMAGE.
// 

using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
#if SILVERLIGHT
using System.Windows;
#endif
using NLog.Config;

namespace NLog.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using NLog.Common;

    /// <summary>
    /// Reflection helpers.
    /// </summary>
    internal static class ReflectionHelpers
    {
        /// <summary>
        /// Gets all usable exported types from the given assembly.
        /// </summary>
        /// <param name="assembly">Assembly to scan.</param>
        /// <returns>Usable types from the given assembly.</returns>
        /// <remarks>Types which cannot be loaded are skipped.</remarks>
        public static Type[] SafeGetTypes(this Assembly assembly)
        {
#if SILVERLIGHT && !WINDOWS_PHONE
            return assembly.GetTypes();
#else
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                foreach (var ex in typeLoadException.LoaderExceptions)
                {
                    InternalLogger.Warn("Type load exception: {0}", ex);
                }

                var loadedTypes = new List<Type>();
                foreach (var t in typeLoadException.Types)
                {
                    if (t != null)
                    {
                        loadedTypes.Add(t);
                    }
                }

                return loadedTypes.ToArray();
            }
#endif
        }


        public static TAttr GetCustomAttribute<TAttr>(this Type type)
            where TAttr : Attribute
        {
#if !UWP10
            return (TAttr)Attribute.GetCustomAttribute(type, typeof(TAttr));
#else

            var typeInfo = type.GetTypeInfo();
            return typeInfo.GetCustomAttribute<TAttr>();
#endif
        }

        /// <summary>
        /// Is this a static class?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>This is a work around, as Type doesn't have this property. 
        /// From: http://stackoverflow.com/questions/1175888/determine-if-a-type-is-static
        /// </remarks>
        public static bool IsStaticClass(this Type type)
        {
            return type.IsClass() && type.IsAbstract() && type.IsSealed();
        }

        public static TAttr GetCustomAttribute<TAttr>(PropertyInfo info)
             where TAttr : Attribute
        {
            return info.GetCustomAttributes(typeof(TAttr), false).FirstOrDefault() as TAttr;
        }

        public static IEnumerable<TAttr> GetCustomAttributes<TAttr>(Type type, bool inherit)
                where TAttr : Attribute
        {
#if !UWP10
            return (TAttr[])Attribute.GetCustomAttributes(type, typeof(TAttr));
#else

            var typeInfo = type.GetTypeInfo();
            return typeInfo.GetCustomAttributes<TAttr>(inherit);
#endif
        }

        public static bool IsDefined<TAttr>(this Type type, bool inherit)
        {
#if !UWP10
            return type.IsDefined(typeof(TAttr), inherit);
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsDefined(typeof(TAttr), inherit);
#endif
        }

        public static bool IsEnum(this Type type)
        {
#if !UWP10
            return type.IsEnum;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsEnum;
#endif
        }

        public static bool IsNestedPrivate(this Type type)
        {
#if !UWP10
            return type.IsNestedPrivate;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsNestedPrivate;
#endif
        }
        public static bool IsGenericTypeDefinition(this Type type)
        {
#if !UWP10
            return type.IsGenericTypeDefinition;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsGenericTypeDefinition;
#endif
        }

        public static bool IsGenericType(this Type type)
        {
#if !UWP10
            return type.IsGenericType;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsGenericType;
#endif
        }
        public static Type GetBaseType(this Type type)
        {
#if !UWP10
            return type.BaseType;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.BaseType;
#endif
        }

        public static bool IsPublic(this Type type)
        {
#if !UWP10
            return type.IsPublic;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsPublic;
#endif
        }


        public static bool IsInterface(this Type type)
        {
#if !UWP10
            return type.IsInterface;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsInterface;
#endif
        }

        public static bool IsAbstract(this Type type)
        {
#if !UWP10
            return type.IsAbstract;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsAbstract;
#endif
        }

        public static bool IsPrimitive(this Type type)
        {
#if !UWP10
            return type.IsPrimitive;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsPrimitive;
#endif
        }
        public static bool IsClass(this Type type)
        {
#if !UWP10
            return type.IsClass;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass;
#endif
        }
        public static bool IsSealed(this Type type)
        {
#if !UWP10
            return type.IsSealed;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsSealed;
#endif
        }



        public static Assembly GetAssembly(this Type type)
        {
#if !UWP10
            return type.Assembly;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.Assembly;
#endif
        }


        public static Module GetModule(this Type type)
        {
#if !UWP10
            return type.Module;
#else
            var typeInfo = type.GetTypeInfo();
            return typeInfo.Module;
#endif
        }
        public static object InvokeMethod(this MethodInfo methodInfo, string methodName, object[] callParameters)
        {
#if !UWP10
            return methodInfo.DeclaringType.InvokeMember(
                 methodName,
                 BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public | BindingFlags.OptionalParamBinding,
                 null,
                 null,
                 callParameters);
#elif !SILVERLIGHT && !UWP10
                , CultureInfo.InvariantCulture
#else


            var neededParameters = methodInfo.GetParameters();

            var missingParametersCount = neededParameters.Length - callParameters.Length;
            if (missingParametersCount > 0)
            {
                //optional parmeters needs to passed here with Type.Missing;
                var paramList = callParameters.ToList();
                paramList.AddRange(Enumerable.Repeat(Type.Missing, missingParametersCount));
                callParameters = paramList.ToArray();
            }
            //TODO test
            return methodInfo.Invoke(methodName, callParameters);
#endif
        }

        public static Assembly GetAssembly(this Module module)
        {
#if !UWP10
            return module.Assembly;
#else
            //TODO check this
            var typeInfo = module.GetType().GetTypeInfo();
            return typeInfo.Assembly;
#endif
        }
#if !UWP10 && !WINDOWS_PHONE

        public static string GetCodeBase(this Assembly assembly)
        {

            return assembly.CodeBase;
        }

#endif

#if !UWP10  && !WINDOWS_PHONE
        public static string GetLocation(this Assembly assembly)
        {
            return assembly.Location;

        }
#endif

#if UWP10
        public static bool IsSubclassOf(this Type type, Type subtype)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsSubclassOf(subtype);

        }
#endif
    }

    /// <summary>
    /// Ext for stackframe
    /// </summary>
    public static class StackFrameExt
    {

#if UWP10
        /// <summary>
        /// Null
        /// </summary>
        /// <returns></returns>
        public static StackFrame GetFrame(this StackTrace strackTrace, int number)
        {

            //TODO
            return null;
        }
#endif
        /// <summary>
        /// 0
        /// </summary>
        /// <returns></returns>
        public static int GetFrameCount(this StackTrace strackTrace)
        {

#if !UWP10
            return strackTrace.FrameCount;
#else
            return 0;

#endif
            //TODO

        }
    }

    /// <summary>
    /// Helpers for <see cref="Assembly"/>.
    /// </summary>
    internal class AssemblyHelpers
    {

#if !UWP10

        /// <summary>
        /// Load from url
        /// </summary>
        /// <param name="assemblyFileName">file or path, including .dll</param>
        /// <param name="baseDirectory">basepath, optional</param>
        /// <returns></returns>
        public static Assembly LoadFromPath(string assemblyFileName, string baseDirectory = null)
        {

#if SILVERLIGHT && !WINDOWS_PHONE
            var stream = Application.GetResourceStream(new Uri(assemblyFileName, UriKind.Relative));
            var assemblyPart = new AssemblyPart();
            Assembly assembly = assemblyPart.Load(stream.Stream);
            return assembly;

#else

            string fullFileName = baseDirectory == null ? assemblyFileName : Path.Combine(baseDirectory, assemblyFileName);

            InternalLogger.Info("Loading assembly file: {0}", fullFileName);

            Assembly asm = Assembly.LoadFrom(fullFileName);
            return asm;
#endif
        }

#endif
        /// <summary>
        /// Load from url
        /// </summary>
        /// <param name="assemblyName">name without .dll</param>
        /// <returns></returns>
        public static Assembly LoadFromName(string assemblyName)
        {
            InternalLogger.Info("Loading assembly: {0}", assemblyName);
#if UWP10 || WINDOWS_PHONE

            var name = new AssemblyName(assemblyName);

            return Assembly.Load(name);


#elif SILVERLIGHT && !WINDOWS_PHONE

            //as embedded resource
            var assemblyFile = assemblyName + ".dll";
            var stream = Application.GetResourceStream(new Uri(assemblyFile, UriKind.Relative));
            var assemblyPart = new AssemblyPart();
            Assembly assembly = assemblyPart.Load(stream.Stream);
            return assembly;

#else



            Assembly assembly = Assembly.Load(assemblyName);
            return assembly;

#endif
        }

    }
}
