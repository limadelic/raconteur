require "fileutils"

def root
  FileUtils.pwd
end

def program_files
  prog_files_x86 = ENV['programfiles(x86)']
  "#{Dir.exist?(prog_files_x86) ? prog_files_x86 : ENV['programfiles']}".gsub(/\\/, '/')
end

def deploy_raconteur
  integration_debug_dir = "#{root}/IDEIntegration/bin/Debug"
  live_dir = "#{root}/live"

  FileUtils.cp_r "#{integration_debug_dir}/IDEIntegration/.", live_dir
  FileUtils.cp_r "#{integration_debug_dir}/ItemTemplates/Templates.zip", "#{live_dir}/RaconteurFeature.zip"

  FileUtils.cp_r "#{integration_debug_dir}/IDEIntegration/.", "#{program_files}/Raconteur"
end

def deploy_resharper_plugin
  plugin_dir = "#{program_files}/JetBrains/ReSharper/v6.0/Bin/Plugins/Raconteur".gsub(/\\/, '/')

  FileUtils.rm_r plugin_dir if Dir.exist? plugin_dir
  FileUtils.mkdir plugin_dir

  FileUtils.cp Dir.glob("#{root}/Resharper/bin/Debug/*Raconteur*.*"), plugin_dir
end

task :default do
  deploy_raconteur
  deploy_resharper_plugin
end