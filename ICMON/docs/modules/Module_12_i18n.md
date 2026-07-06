# Module 12: Multi-Language (i18n)

## Overview
ระบบรองรับหลายภาษา (18 ภาษา) จัดการข้อความที่แปลแล้ว สลับภาษา

## Supported Languages

| Code | Language |
|------|----------|
| `th` | ภาษาไทย |
| `en` | English |
| `zh` | 中文 |
| `ja` | 日本語 |
| `ko` | 한국어 |
| `es` | Español |
| `fr` | Français |
| `de` | Deutsch |
| `it` | Italiano |
| `pt` | Português |
| `ru` | Русский |
| `ar` | العربية (RTL) |
| `hi` | हिन्दी |
| `id` | Bahasa Indonesia |
| `ms` | Bahasa Melayu |
| `vi` | Tiếng Việt |
| `my` | မြန်မာဘာသာ |
| `lo` | ພາສາລາວ |

## Database Tables

| Table | Description |
|-------|-------------|
| `m_language` | ข้อมูลภาษา (language_code, locale, is_rtl) |
| `m_translation` | ข้อความที่แปลแล้ว (message_key, language_code, message_text) |

## API Endpoints

| Method | Path | Rate Limit |
|--------|------|------------|
| GET | `/api/languages` | 50/60s |
| GET | `/api/languages/current` | 100/60s |
| POST | `/api/languages/switch` | 20/60s |
| GET | `/api/languages/messages/{lang}` | 30/60s |

## Redis Cache Keys

| Key | TTL |
|-----|-----|
| `messages:{messageKey}:{lang}` | 1 hr |
| `messages_all:{lang}` | 1 hr |
| `languages:{languageCode}` | 2 hr |

## Key Features

- ✅ 18 languages supported
- ✅ RTL support for Arabic
- ✅ JSON resource files
- ✅ Translation management via API
- ✅ Cache with Redis
- ✅ Auto-detect browser language
- ✅ Language persistence per user
