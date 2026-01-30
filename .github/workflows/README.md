# GitHub Actions Workflows - Invoice Sync

## 🎯 Workflows Disponibles

### 1. `invoice-sync-pr.yml` - PR Validation
- **Trigger:** Pull requests a `main` que modifican `src/InvoiceSync/**`
- **Acción:** Compila imagen Docker (NO publica)
- **Propósito:** Validación rápida antes de merge

### 2. `invoice-sync-release.yml` - Release
- **Trigger:** Push de tags con formato `invoice-sync/v*`
- **Acción:** Build + Push a GHCR + GitHub Release
- **Tags publicados:** `1.2.3` + `latest`

---

## 🚀 Cómo Hacer un Release

```bash
# 1. Asegúrate de estar en main actualizado
git checkout main
git pull origin main

# 2. Crea tag con prefijo de proyecto
git tag invoice-sync/v1.0.0

# 3. Push del tag
git push origin invoice-sync/v1.0.0
```

**GitHub Actions automáticamente:**
- ✅ Construye imagen Docker con versión `1.0.0`
- ✅ Publica a `ghcr.io/renzojared/mcp-invoice-sync:1.0.0`
- ✅ Publica a `ghcr.io/renzojared/mcp-invoice-sync:latest`
- ✅ Crea GitHub Release con changelog automático
- ✅ Genera attestation de seguridad

---

## 📦 Usar Imágenes Publicadas

```bash
# Versión específica
docker pull ghcr.io/renzojared/mcp-invoice-sync:1.0.0

# Última versión
docker pull ghcr.io/renzojared/mcp-invoice-sync:latest

# Ver metadata
docker inspect ghcr.io/renzojared/mcp-invoice-sync:1.0.0
```

---

## 🔍 Monitorear Workflows

1. Ve a: https://github.com/renzojared/mcp-poc/actions
2. Selecciona workflow que quieres ver
3. Click en run específico para logs detallados

---

## ❓ Troubleshooting

### Error: "Resource not accessible by integration"
**Solución:** Verifica que el repositorio tenga habilitado:
- Settings → Actions → General → Workflow permissions: "Read and write"

### La imagen no aparece en mi perfil
**Solución:** Hace visible el package:
1. https://github.com/renzojared?tab=packages
2. Click en `mcp-invoice-sync`
3. Package settings → Change visibility → Public

### Build local tiene versión 0.0.0-dev
**Esto es correcto.** Builds locales son para desarrollo.
Solo imágenes de CI/CD tienen versiones oficiales.

---

## 🎯 Agregar Nuevo Proyecto (ProjectB)

1. Copia workflows:
   ```bash
   cp invoice-sync-pr.yml projectb-pr.yml
   cp invoice-sync-release.yml projectb-release.yml
   ```

2. Edita `projectb-pr.yml`:
   ```yaml
   paths:
     - 'src/ProjectB/**'
   ```

3. Edita `projectb-release.yml`:
   ```yaml
   on:
     push:
       tags:
         - 'projectb/v*'
   
   env:
     IMAGE_NAME: ${{ github.repository_owner }}/mcp-projectb
   ```

4. Agrega `ARG VERSION` al Dockerfile de ProjectB

**¡Listo!** ProjectB tendrá CI/CD independiente.
