
#### Sidebar Examples

```razor
<!-- Sidebar example that comprehensively demonstration the majority of features. This includes logos, icons, dropdowns, item grouping, responsiveness, CTA region with a button -->
<div class="flex">
        <!-- Mobile menu button -->
        <Button Color="ButtonColor.Dark" class="lg:hidden mb-3">
            <BarsIcon class="w-5 h-5" />
        </Button>
    </div>

    <Sidebar CollapseMode="SidebarCollapseMode.Responsive">
        <SidebarLogo 
            Href="#"
            ImgSrc="/images/logo.svg">
            Flowbite
        </SidebarLogo>

        <SidebarItemGroup>
            <SidebarItem 
                Href="#"
                Icon="@(new HomeIcon())">
                Dashboard
            </SidebarItem>
            
            <SidebarCollapse 
                Label="Analytics"
                Icon="@(new ChartPieIcon())">
                <SidebarItem 
                    Href="#"
                    Icon="@(new ChartLineUpIcon())">
                    Overview
                </SidebarItem>
                <SidebarItem 
                    Href="#"
                    Icon="@(new ChartMixedIcon())">
                    Reports
                </SidebarItem>
            </SidebarCollapse>
            
            <SidebarItem 
                Href="#"
                Icon="@(new BriefcaseIcon())">
                Users
            </SidebarItem>
            
            <SidebarItem 
                Href="#"
                Icon="@(new CartIcon())">
                Products
            </SidebarItem>
        </SidebarItemGroup>

        <SidebarItemGroup>
            <SidebarItem 
                Href="#"
                Icon="@(new CogIcon())">
                Settings
            </SidebarItem>
            
            <SidebarItem 
                Href="#"
                Icon="@(new BellIcon())">
                Help Center
            </SidebarItem>
        </SidebarItemGroup>

        <SidebarCTA>
            <div class="mb-3 text-sm text-blue-900 dark:text-blue-400">
                Preview the new Flowbite dashboard navigation! You can turn the new navigation off for a limited time in your profile.
            </div>
            <Button Color="ButtonColor.Default" class="w-full">
                Upgrade to Pro
            </Button>
        </SidebarCTA>
    </Sidebar>


<!-- Mulit-Level Sidebar  that demonstrates deep nested navigation with multiple levels of dropdowns, perfect for complex application hierarchies. -->
<Sidebar>
    <SidebarItem 
        Href="/dashboard"
        Icon="@(new HomeIcon())">
        Dashboard
    </SidebarItem>
    
    <SidebarCollapse 
        Label="Settings"
        Icon="@(new CogIcon())">
        <SidebarItem 
            Href="/settings/profile"
            Icon="@(new BriefcaseIcon())">
            Profile
        </SidebarItem>
        <SidebarCollapse 
            Label="System"
            Icon="@(new ServerIcon())">
            <SidebarItem 
                Href="/settings/system/general"
                Icon="@(new AdjustmentsHorizontalIcon())">
                General
            </SidebarItem>
            <SidebarCollapse 
                Label="Security"
                Icon="@(new ShieldCheckIcon())">
                <SidebarItem 
                    Href="/settings/system/security/permissions"
                    Icon="@(new BadgeCheckIcon())">
                    Permissions
                </SidebarItem>
                <SidebarItem 
                    Href="/settings/system/security/authentication"
                    Icon="@(new BellIcon())">
                    Authentication
                </SidebarItem>
                <SidebarItem 
                    Href="/settings/system/security/encryption"
                    Icon="@(new ShieldCheckIcon())">
                    Encryption
                </SidebarItem>
            </SidebarCollapse>
            <SidebarItem 
                Href="/settings/system/backup"
                Icon="@(new CloudArrowUpIcon())">
                Backup
            </SidebarItem>
        </SidebarCollapse>
        <SidebarItem 
            Href="/settings/notifications"
            Icon="@(new BellIcon())">
            Notifications
        </SidebarItem>
    </SidebarCollapse>
    
    <SidebarItem 
        Href="/help"
        Icon="@(new BellIcon())">
        Help
    </SidebarItem>
</Sidebar>

```



