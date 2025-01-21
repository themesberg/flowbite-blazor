
</doc>

<doc title="Using Icons" description="Working with Flowbite Blazor icons">

## Icon Packages

Flowbite Blazor provides two icon packages:

1. Core Icons - Built into the main package
2. Extended Icons - Additional icons in a separate package

### Basic Usage

```razor
<!-- Using core icons -->
<Button>
    <HomeIcon class="w-5 h-5 mr-2" />
    Home
</Button>

<!-- Using icons in components -->
<Alert Icon="@(new InfoIcon())" Color="AlertColor.Info">
    Important information
</Alert>

<!-- Using icons in components -->
<Button Icon="@(new HomeIcon())" Color="ButtonColor.Primary">
    Home
</Button>
```

### List of Icon Components

The Icon class name is defined as `{{name}}Icon` where `name` is from the following list:
- Apple
- ArrowDown
- ArrowLeft
- ArrowRight
- ArrowRightToBracket
- ArrowUp
- ArrowUpRightFromSquare
- Aws
- Bars
- Bell
- Bluesky
- CalendarMonth
- Chart
- CheckCircle
- Check
- ChevronDown
- ChevronLeft
- ChevronRight
- ChevronUp
- ClipboardArrow
- Clock
- CloseCircle
- CloseCircleSolid
- Close
- CodeBranch
- Compress
- Database
- Discord
- DotsHorizontal
- DotsVertical
- Download
- Edit
- Envelope
- ExclamationSolid
- ExclamationTriangle
- Expand
- Eye
- EyeSlash
- Facebook
- FileCopy
- FileExport
- File
- FileImport
- Filter
- FloppyDiskAlt
- FloppyDisk
- Folder
- Forward
- Gear
- Github
- Gitlab
- Google
- Grid
- Hamburger
- Heart
- Home
- Image
- InfoCircle
- Info
- Instagram
- Linkedin
- List
- Lock
- LockOpen
- MapPin
- Messages
- PaperClip
- Pencil
- Phone
- Play
- Plus
- Printer
- QuestionCircle
- Reddit
- Refresh
- Rocket
- Search
- ShareNodes
- Sort
- Star
- Stop
- TableRow
- TrashBin
- Twitter
- Undo
- Upload
- UserCircle
- User
- UserSolid
- Whatsapp
- Windows
- X
- Youtube


</doc>
